using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IIndicatorIndexCityValueService : ISimpleCRUDService<IndicatorIndexCityValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        Task SaveIndicatorIndexesChanges(IndicatorIndexValueModel[] values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valRec"></param>
        /// <returns></returns>
        Task ApplyIndicatorIndexValue(IndicatorIndexValueModel valRec);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indicatorId"></param>
        /// <returns></returns>
        Task<IEnumerable<IndicatorIndexCityValue>> GetByIndicator(long indicatorId);
    }
}
