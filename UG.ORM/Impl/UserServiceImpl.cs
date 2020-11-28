using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;

using UG.Model;
using UG.ORM.Base;
using UG.Configuration;

namespace UG.ORM.Impl
{
    public class UserServiceImpl : BaseCRUDService<User>, IUserService
    {
        public UserServiceImpl(IOptions<ConnectionStringsConfiguration> optionsAccessor) : base(optionsAccessor) { }

        public async Task<User> ExplicitGet(long userId)
        {
            var conn = GetMySqlConnection();
            string sql = @"
SELECT * FROM `User` WHERE UserId=@UserId;";
            var users = await conn.QueryAndCloseConnAsync<User>(sql, new { UserId = userId });
            var res = users.FirstOrDefault();
            return res;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            var conn = GetMySqlConnection();
            string sql = @"
SELECT * 
FROM `User`
WHERE UserName=@UserName;";

            var users = await conn.QueryAndCloseConnAsync<User>(sql, new { UserName = login });
            var res = users.FirstOrDefault();
            return res;
        }        
    }
}
