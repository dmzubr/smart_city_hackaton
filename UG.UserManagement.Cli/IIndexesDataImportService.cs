using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Serilog;

using UG.Model;
using UG.ORM;

namespace UG.Utils.Cli
{
    public interface IIndexesDataImportService
    {
        Task ImportIndexesData(string filePath);
    }

    public class IndexesDataImportServiceImpl : IIndexesDataImportService
    {
        private readonly ISubIndexService _subIndexService;
        private readonly IIndicatorService _indicatorService;
        private readonly IIndicatorIndexService _indicatorIndexService;

        public IndexesDataImportServiceImpl(ISubIndexService subIndexService,
            IIndicatorService indicatorService,
            IIndicatorIndexService indicatorIndexService)
        {
            this._subIndexService = subIndexService;
            this._indicatorService = indicatorService;
            this._indicatorIndexService = indicatorIndexService;
        }

        public async Task ImportIndexesData(string filePath)
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            var headerRowsCount = 2;
            lines = lines.Skip(headerRowsCount).ToArray();

            SubIndex currentSubIndex = null;
            Indicator currentIndicator = null;
            foreach (var line in lines)
            {
                var spl = line.Split(new char[] { '\t' });
                
                if (spl.Where(X => X.Length == 0).Count() > 5 && spl[0].Length > 0)
                {
                    Log.Debug("------------ Create subindex: {newIndexName}", spl[0]);
                    currentSubIndex = new SubIndex { Name = spl[0] };
                    await this._subIndexService.AddAndAssignId(currentSubIndex);
                    continue;                    
                }
                
                if (!string.IsNullOrEmpty(spl[1]))
                {
                    Log.Debug("----- Create indicator: {newIndicatorName}", spl[1]);
                    currentIndicator = new Indicator 
                    { 
                        Number = int.Parse(spl[0]),
                        Name = spl[1],
                        CalculationDescription = spl[4],
                        SubIndexId = currentSubIndex.SubIndexId
                    };
                    await this._indicatorService.AddAndAssignId(currentIndicator);
                }

                Log.Debug("Create indicator index: {indicatorIndexName}", spl[10]);
                var indicatorIndex = new IndicatorIndex
                {
                    IndicatorId = currentIndicator.IndicatorId,
                    Name = spl[10],
                    Description = spl[14],
                    Number = spl[9],
                    Type = spl[11]
                };
                await this._indicatorIndexService.Add(indicatorIndex);
            }
        }        
    }    
}
