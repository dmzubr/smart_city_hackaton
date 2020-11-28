using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class SubIndexServiceImpl : BaseCRUDService<SubIndex>, ISubIndexService
    {
        private readonly IIndicatorService _indicatorService;
        private readonly IIndicatorIndexService _indicatorIndexService;

        public SubIndexServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor,
            IIndicatorService indicatorService,
            IIndicatorIndexService indicatorIndexService) : base(optionsAccessor) 
        {
            this._indicatorService = indicatorService;
            this._indicatorIndexService = indicatorIndexService;
        }

        public async Task AddAndAssignId(SubIndex rec)
        {
            var conn = GetMySqlConnection();
            var sql = @"
INSERT INTO `SubIndex` (`Name`) 
VALUES (@Name);
SELECT LAST_INSERT_ID();";
            var execRes = await conn.QueryAndCloseConnAsync<int>(sql, new
            {
                Name = rec.Name,
            });
            var res = execRes.FirstOrDefault();
            rec.SubIndexId = res;
        }

        public async Task<IEnumerable<SubIndexViewModel>> GetVMList()
        {
            var res = new List<SubIndexViewModel>();

            var subIndexes = await this.GetList();
            var indicators = await this._indicatorService.GetList();
            var indicatorIndexes = await this._indicatorIndexService.GetList();            

            foreach(var subindex in subIndexes)
            {
                var rec = new SubIndexViewModel { Name = subindex.Name, SubIndexId = subindex.SubIndexId };
                var relatedIndicators = indicators.Where(X => X.SubIndexId == subindex.SubIndexId);
                var indicatorsVMList = new List<IndicatorViewModel>();
                foreach(var indicator in relatedIndicators)
                {
                    var relatedIndicatorIndexes = indicatorIndexes.Where(X => X.IndicatorId == indicator.IndicatorId);
                    var indicatorVMRec = new IndicatorViewModel
                    {
                        IndicatorId = indicator.IndicatorId,
                        Name = indicator.Name,
                        Number = indicator.Number,
                        SubIndexId = indicator.SubIndexId,
                        CalculationDescription = indicator.CalculationDescription,
                        indicatorIndexes = relatedIndicatorIndexes.ToArray()
                    };
                    indicatorsVMList.Add(indicatorVMRec);
                }
                rec.indicatorsList = indicatorsVMList.ToArray();
                res.Add(rec);
            }

            return res;
        }
    }
}
