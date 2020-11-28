using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class OuterMetricServiceImpl : BaseCRUDService<OuterMetric>, IOuterMetricService
    {
        public OuterMetricServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }        

        public async Task<OuterMetric> GetByCode(string metricCode)
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT * FROM `OuterMetric`
WHERE Code=@Code;";

            var execRes = await conn.QueryAndCloseConnAsync<OuterMetric>(sql, new { Code = metricCode });
            var res = execRes.FirstOrDefault();
            return res;
        }    
    }
}
