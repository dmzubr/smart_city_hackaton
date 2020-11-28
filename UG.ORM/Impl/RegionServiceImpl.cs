using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class RegionServiceImpl : BaseCRUDService<Region>, IRegionService
    {
        public RegionServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }

        public async Task AddAndAssignId(Region rec)
        {
            var conn = GetMySqlConnection();
            var sql = @"
INSERT INTO `Region` (`Name`) VALUES (@Name);
SELECT LAST_INSERT_ID();";

            var execRes = await conn.QueryAndCloseConnAsync<int>(sql, new { Name = rec.Name });
            var res = execRes.FirstOrDefault();
            rec.RegionId = res;
        }
    }
}
