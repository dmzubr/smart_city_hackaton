using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IIndicatorCityValueService : ISimpleCRUDService<IndicatorCityValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IndicatorValuesContainer> GetValuesContainer();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="indicatorId"></param>
        /// <param name="cityId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        Task RecalculateValue(long indicatorId, int cityId, int year);
    }
}
