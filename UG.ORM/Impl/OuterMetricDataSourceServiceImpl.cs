using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class OuterMetricDataSourceServiceImpl : BaseCRUDService<OuterMetricDataSource>, IOuterMetricDataSourceService
    {
        public OuterMetricDataSourceServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }

        public async Task<IEnumerable<OuterMetricDataSourceViewModel>> GetVMList()
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT
    S.*, 
    M.Name as MetricName
FROM
    `OuterMetric` M INNER JOIN
    `OuterMetricDataSource` S ON S.OuterMetricId=M.OuterMetricId;";
            var res = await conn.QueryAndCloseConnAsync<OuterMetricDataSourceViewModel>(sql);            
            return res;
        }
    }
}
