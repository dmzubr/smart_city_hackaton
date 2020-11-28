using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UG.ORM;
using UG.WebApi.Models;

namespace UG.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndicatorSocialVerificationController : Controller
    {
        private readonly IIndicatorSocialVerificationService _indicatorSocialVerification;

        public IndicatorSocialVerificationController(IIndicatorSocialVerificationService indicatorSocialVerification)
        {
            this._indicatorSocialVerification = indicatorSocialVerification;
        }
                
        [HttpGet]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> GetValues([FromQuery] int year)
        {
            var res = await this._indicatorSocialVerification.GetListByYear(year);
            var resList = res.OrderBy(X => X.Value).ToList();

            var classesCount = 3;
            var classLength = resList.Count / classesCount;

            for (var i=0; i < resList.Count(); i++)
            {
                var pos = i + 1;
                resList[i].RatingPosition = pos;
                
                double quotient = pos / (double)classLength;
                double ceiling = Math.Ceiling(quotient);
                int ratingClass = (int)ceiling;

                resList[i].RatingClass = ratingClass; //(pos+1 / classLength) + 1;
            }            
            
            return Json(resList);
        }
    }
}
