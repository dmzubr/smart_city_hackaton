using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;
using System.Collections.Generic;

namespace UG.ORM.Impl
{
    public class IndicatorIndexCityValueServiceImpl : BaseCRUDService<IndicatorIndexCityValue>, IIndicatorIndexCityValueService
    {
        private readonly IIndicatorIndexService _indicatorIndexService;

        public IndicatorIndexCityValueServiceImpl(
            IOptions<ConnectionStringsConfiguration> optionsAccessor,
            IIndicatorIndexService indicatorIndexService) : base(optionsAccessor) 
        {
            this._indicatorIndexService = indicatorIndexService;
        }

        public async Task SaveIndicatorIndexesChanges(IndicatorIndexValueModel[] values)
        {
            if (values.Length == 0)
                return;
            
            foreach (var rec in values)
            {
                await this.ApplyIndicatorIndexValue(rec);                
            }
        }

        public async Task ApplyIndicatorIndexValue(IndicatorIndexValueModel rec)
        {
            var createdvalueRec = new IndicatorIndexCityValue
            {
                CityId = rec.CityId,
                Value = rec.Value,
                CalcDate = DateTime.Now,
                IndicatorIndexId = rec.IndicatorIndexId,
                PeriodStart = new DateTime(rec.Year, 1, 1),
                PeriodEnd = new DateTime(rec.Year, 12, 31)
            };
            await this.Add(createdvalueRec);            
        }

        public async Task<IEnumerable<IndicatorIndexCityValue>> GetByIndicator(long indicatorId)
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT IV.*
FROM
    `IndicatorIndexCityValue` IV INNER JOIN
    `IndicatorIndex` I ON IV.IndicatorIndexId=I.IndicatorIndexId
WHERE I.IndicatorId=@IndicatorId;";
            var res = await conn.QueryAndCloseConnAsync<IndicatorIndexCityValue>(sql, new { IndicatorId = indicatorId });            
            return res;
        }
    }
}
