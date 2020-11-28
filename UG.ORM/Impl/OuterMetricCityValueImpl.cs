using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;
using System.Collections.Generic;

namespace UG.ORM.Impl
{
    public class OuterMetricCityValueImpl : BaseCRUDService<OuterMetricCityValue>, IOuterMetricCityValueService
    {
        public OuterMetricCityValueImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }                

        public async Task<IEnumerable<OuterMetricValueViewModel>> GetVMListByMetric(long outerMetricId)
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT
    MV.*,
    M.Name as MetricName,
    C.Name as CityName
FROM 
    `OuterMetricCityValue` MV INNER JOIN
    `OuterMetric` M ON MV.OuterMetricId=M.OuterMetricId INNER JOIN
    `City` C ON MV.CityId=C.CityId
WHERE MV.OuterMetricId=@MetricId;";
            var res = await conn.QueryAndCloseConnAsync<OuterMetricValueViewModel>(sql, new { MetricId = outerMetricId });
            return res;
        }
    }
}
