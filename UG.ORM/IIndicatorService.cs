using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IIndicatorService : ISimpleCRUDService<Indicator>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        Task AddAndAssignId(Indicator rec);        
    }
}
