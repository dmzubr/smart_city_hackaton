using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UG.ORM;

namespace UG.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OuterMetricDataSourceController : Controller
    {
        private readonly IOuterMetricDataSourceService _outerMetricDataSourceService;

        public OuterMetricDataSourceController(
            IOuterMetricDataSourceService outerMetricDataSourceService)
        {
            this._outerMetricDataSourceService = outerMetricDataSourceService;
        }
                
        [HttpGet]
        [Authorize(Roles = "admin,central_manager")]
        public async Task<IActionResult> GetVMList()
        {
            var res = await this._outerMetricDataSourceService.GetVMList();
            return Json(res);
        }
    }
}
