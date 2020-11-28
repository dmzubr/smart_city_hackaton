using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;
using System;
using Serilog;

namespace UG.ORM.Impl
{
    public class IndicatorCityValueServiceImpl : BaseCRUDService<IndicatorCityValue>, IIndicatorCityValueService
    {
        private readonly IIndicatorIndexCityValueService _indicatorIndexValueService;
        private readonly IIndicatorIndexService _indicatorIndexService;

        public IndicatorCityValueServiceImpl(
            IOptions<ConnectionStringsConfiguration> optionsAccessor,
            IIndicatorIndexCityValueService indicatorIndexValueService,
            IIndicatorIndexService indicatorIndexService) : base(optionsAccessor) 
        {
            this._indicatorIndexValueService = indicatorIndexValueService;
            this._indicatorIndexService = indicatorIndexService;
        }

        public async Task<IndicatorValuesContainer> GetValuesContainer()
        {
            var allIndicatorsIndexesValues = await this._indicatorIndexValueService.GetList();
            var allIndicatorsValues = await this.GetList();

            var res = new IndicatorValuesContainer();
            res.indicatorValuesList = allIndicatorsValues.Select(X => new IndicatorValueModel
            {
                CityId = X.CityId,
                Value = X.Value,
                IndicatorId = X.IndicatorId,
                Year = X.PeriodStart.Year
            }).ToList();
            res.indicatorIndexesValuesList = allIndicatorsIndexesValues.Select(X => new IndicatorIndexValueModel
            {
                CityId = X.CityId,
                Value = X.Value,
                IndicatorIndexId = X.IndicatorIndexId,
                Year = X.PeriodStart.Year
            }).ToList();

            return res;
        }

        private async Task<IndicatorCityValue> GetByCityAndYear(long indicatorId, int cityId, int year)
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT * FROM `IndicatorCityValue`
WHERE 
    IndicatorId=@IndicatorId
    AND CityId=@CityId;";
            var execRes = await conn.QueryAndCloseConnAsync<IndicatorCityValue>(sql, new
            { 
                IndicatorId = indicatorId, 
                CityId = cityId
            });
            var res = execRes.FirstOrDefault(X => X.PeriodStart.Year == year);
            return res;
        }

        public async Task RecalculateValue(long indicatorId, int cityId, int year)
        {            
            var childIndexes = await this._indicatorIndexValueService.GetByIndicator(indicatorId);
            var allIndexes = await this._indicatorIndexService.GetByIndicator(indicatorId);
            var newIndicatorVal = childIndexes.Sum(X => X.Value) / allIndexes.Count();

            var indicatorValRec = await this.GetByCityAndYear(indicatorId, cityId, year);
            if (indicatorValRec != null)
            {
                indicatorValRec.Value = newIndicatorVal;
                await this.Update(indicatorValRec);
                Log.Information("Indicator value record updated");
            }
            else
            {
                indicatorValRec = new IndicatorCityValue
                {
                    CityId = cityId,
                    CalcDate = DateTime.Now,
                    IndicatorId = indicatorId,
                    Value = newIndicatorVal,
                    PeriodStart = new DateTime(year, 1, 1),
                    PeriodEnd = new DateTime(year, 12, 31)
                };
                await this.Add(indicatorValRec);
                Log.Information("Indicator value record created");
            }
        }
    }
}
