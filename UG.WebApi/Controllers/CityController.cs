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
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            this._cityService = cityService;
        }
                
        [HttpGet]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> GetList()
        {
            var res = await this._cityService.GetList();
            return Json(res);
        }
    }
}
