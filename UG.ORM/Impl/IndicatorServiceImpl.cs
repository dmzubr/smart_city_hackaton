using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class IndicatorServiceImpl : BaseCRUDService<Indicator>, IIndicatorService
    {
        public IndicatorServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }

        public async Task AddAndAssignId(Indicator rec)
        {
            var conn = GetMySqlConnection();
            var sql = @"
INSERT INTO `Indicator` (
    `SubIndexId`,
    `Name`,
    `Number`,        
    `CalculationDescription`
) 
VALUES (    
    @SubIndexId,
    @Name,
    @Number,        
    @CalculationDescription
);
SELECT LAST_INSERT_ID();";

            var execRes = await conn.QueryAndCloseConnAsync<long>(sql, new
            {
                SubIndexId = rec.SubIndexId,
                Name = rec.Name,
                Number = rec.Number,
                CalculationDescription = rec.CalculationDescription
            });
            var res = execRes.FirstOrDefault();
            rec.IndicatorId = res;
        }
    }
}
