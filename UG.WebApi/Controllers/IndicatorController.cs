using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UG.Model;
using UG.ORM;

namespace UG.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndicatorController : Controller
    {
        private readonly IIndicatorService _indicatorService;

        public IndicatorController(IIndicatorService indicatorService)
        {
            this._indicatorService = indicatorService;
        }
                
        [HttpGet]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> Get([FromQuery] long indicatorId)
        {
            var res = await this._indicatorService.Get(indicatorId);
            return Json(res);
        }
    }
}
