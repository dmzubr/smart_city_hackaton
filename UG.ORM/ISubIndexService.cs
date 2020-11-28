using System.Collections.Generic;
using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface ISubIndexService : ISimpleCRUDService<SubIndex>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        Task AddAndAssignId(SubIndex rec);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SubIndexViewModel>> GetVMList();
    }
}
