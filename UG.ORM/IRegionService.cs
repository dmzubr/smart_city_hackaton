using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IRegionService : ISimpleCRUDService<Region>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        Task AddAndAssignId(Region rec);
    }
}
