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
    public class IndicatorValueController : Controller
    {
        private readonly IIndicatorCityValueService _indicatorValueService;

        public IndicatorValueController(IIndicatorCityValueService indicatorValueService)
        {
            this._indicatorValueService = indicatorValueService;
        }
                
        [HttpGet]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> GetValuesContainer()
        {
            var res = await this._indicatorValueService.GetValuesContainer();
            return Json(res);
        }
    }
}
