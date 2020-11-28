using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IOuterMetricDataSourceService : ISimpleCRUDService<OuterMetricDataSource>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OuterMetricDataSourceViewModel>> GetVMList();
    }
}
