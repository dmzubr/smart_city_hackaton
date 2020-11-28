using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;
using System.Collections.Generic;

namespace UG.ORM.Impl
{
    public class IndicatorIndexServiceImpl : BaseCRUDService<IndicatorIndex>, IIndicatorIndexService
    {
        public IndicatorIndexServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }

        public async Task<IEnumerable<IndicatorIndex>> GetByIndicator(long indicatorId)
        {
            var conn = GetMySqlConnection();
            var sql = @"
SELECT * 
FROM `IndicatorIndex`
WHERE IndicatorId=@IndicatorId;";
            var res = await conn.QueryAndCloseConnAsync<IndicatorIndex>(sql, new { IndicatorId = indicatorId });
            return res;
        }
    }
}
