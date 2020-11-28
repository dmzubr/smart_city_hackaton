using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class IndicatorCityValueServiceImpl : BaseCRUDService<IndicatorCityValue>, IIndicatorCityValueService
    {
        private readonly IIndicatorIndexCityValueService _indicatorIndexValueService;

        public IndicatorCityValueServiceImpl(
            IOptions<ConnectionStringsConfiguration> optionsAccessor,
            IIndicatorIndexCityValueService indicatorIndexValueService) : base(optionsAccessor) 
        {
            this._indicatorIndexValueService = indicatorIndexValueService;
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
    }
}
