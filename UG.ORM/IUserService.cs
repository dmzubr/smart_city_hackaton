using System.Threading.Tasks;

using UG.Model;
using UG.ORM.Base;

namespace UG.ORM
{
    public interface IUserService : ISimpleCRUDService<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> ExplicitGet(long userId);

        /// <summary>
        /// Get instance of user by login (=UserName)
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<User> GetUserByLogin(string login);
    }
}
