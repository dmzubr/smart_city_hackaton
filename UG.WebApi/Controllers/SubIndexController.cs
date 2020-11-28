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
    public class SubIndexController : Controller
    {
        private readonly ISubIndexService _subIndexService;

        public SubIndexController(ISubIndexService subIndexService)
        {
            this._subIndexService = subIndexService;
        }
                
        [HttpGet]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> GetVMList()
        {
            var res = await this._subIndexService.GetVMList();
            return Json(res);
        }
    }
}
