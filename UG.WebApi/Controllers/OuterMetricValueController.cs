using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UG.ORM;

namespace UG.WebApi.Controllers
{
    public class HandleOuterMetricsValueResponseModel
    {
        public int not_found_metric_code { get; set; } = 0;

        public int not_found_city { get; set; } = 0;

        public int succsesfully_created { get; set; } = 0;
    }

    [Route("api/[controller]/[action]")]    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OuterMetricValueController : Controller
    {
        private readonly IOuterMetricService _outerMetricService;
        private readonly IOuterMetricCityValueService _outerMetricCityValueService;
        private readonly ICityService _cityService;

        public OuterMetricValueController(
            IOuterMetricService outerMetricService,
            IOuterMetricCityValueService outerMetricCityValueService,
            ICityService cityService)
        {
            this._outerMetricService = outerMetricService;
            this._outerMetricCityValueService = outerMetricCityValueService;
            this._cityService = cityService;
        }
                
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> HandleScrapperOne([FromBody] MetricValueSubmitBodyModel req)
        {
            Log.Debug("HandleScrapperOne req body is: {@req}", req);

            var metricsRecs = await this._outerMetricService.GetList();

            var receivedMetricsCodes = req.values.Select(X => X.metric_code);            
            foreach (var metricCode in receivedMetricsCodes)
            {
                var metricRec = metricsRecs.FirstOrDefault(X => string.Equals(X.Code, metricCode, StringComparison.OrdinalIgnoreCase));
                if (metricRec == null)
                    return BadRequest($"Metric code {metricCode} is not supported");
            }

            var res = new HandleOuterMetricsValueResponseModel();

            var cities = await this._cityService.GetList();
            foreach (var metricVal in req.values)
            {
                var metricRec = metricsRecs.FirstOrDefault(X => string.Equals(X.Code, metricVal.metric_code, StringComparison.OrdinalIgnoreCase));
                if (metricRec == null )
                {
                    res.not_found_metric_code++;
                    continue;
                }
                
                var city = cities.FirstOrDefault(X => string.Equals(X.OKTMO, metricVal.oktmo, StringComparison.OrdinalIgnoreCase));
                if (city == null)
                {
                    res.not_found_city++;
                    continue;
                }
                var valRecord = new UG.Model.OuterMetricCityValue
                {
                    CityId = city.CityId,
                    CalcDate = DateTime.Now,
                    PeriodStart = metricVal.period_start,
                    PeriodEnd = metricVal.period_end,
                    OuterMetricId = metricRec.OuterMetricId,
                    Value = metricVal.value
                };
                await this._outerMetricCityValueService.Add(valRecord);
                res.succsesfully_created++;
            }

            return Json(res);
        }

        [HttpGet]
        [Authorize(Roles = "admin,region_manager,central_manager")]
        public async Task<IActionResult> GetValuesByMetric([FromQuery] long outerMetricId)
        {
            var res = await this._outerMetricCityValueService.GetVMListByMetric(outerMetricId);
            return Json(res);
        }
    }
}
