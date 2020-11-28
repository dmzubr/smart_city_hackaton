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
    }
}
