using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IIndicatorIndexService : ISimpleCRUDService<IndicatorIndex>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IndicatorIndex>> GetByIndicator(long indicatorId);
    }
}
