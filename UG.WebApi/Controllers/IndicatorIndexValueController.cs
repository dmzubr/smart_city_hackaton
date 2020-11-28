using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UG.Model;
using UG.ORM;

namespace UG.WebApi.Controllers
{
    public class SaveIndicatorIndexesChangesReqBody
    {
        public IndicatorIndexValueModel[] values { get; set; }
    }


    [Route("api/[controller]/[action]")]    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndicatorIndexValueController : Controller
    {
        private readonly IIndicatorIndexCityValueService _indicatorIndexCityValueService;
        private readonly IIndicatorCityValueService _indicatorValueService;
        private readonly IIndicatorIndexService _indicatorIndexService;

        public IndicatorIndexValueController(
            IIndicatorIndexCityValueService indicatorIndexCityValueService,
            IIndicatorCityValueService indicatorValueService,
            IIndicatorIndexService indicatorIndexService)
        {
            this._indicatorIndexCityValueService = indicatorIndexCityValueService;
            this._indicatorValueService = indicatorValueService;
            this._indicatorIndexService = indicatorIndexService;
        }
                
        [HttpPost]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> SaveIndicatorIndexesChanges([FromBody]  SaveIndicatorIndexesChangesReqBody req)
        {
            await this._indicatorIndexCityValueService.SaveIndicatorIndexesChanges(req.values);
            return Json(true);
        }

        [HttpPost]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> ApplyIndicatorIndexValue([FromBody] IndicatorIndexValueModel req)
        {
            await this._indicatorIndexCityValueService.ApplyIndicatorIndexValue(req);
            var relatedIndicatorIndex = await this._indicatorIndexService.Get(req.IndicatorIndexId);
            await this._indicatorValueService.RecalculateValue(relatedIndicatorIndex.IndicatorId, req.CityId, req.Year);
            return Json(true);
        }        
    }
}
