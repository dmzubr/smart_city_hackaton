using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Serilog;
using AspNetCore.Identity.MySQLDapper.Model;

using UG.ORM;

namespace UG.Utils.Cli
{
    public interface IUserCreatorService
    {
        Task CreateUserRecord(string userName, string password, string[] rolesList);

        Task ChangePass(string userName, string oldPassword, string newPassword);
    }

    public class UserCreatorImpl : IUserCreatorService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UserCreatorImpl(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task CreateUserRecord(string userName, string password, string[] rolesList)
        {
            var appUser = new ApplicationUser
            {
                UserName = userName,
                RegistrationDate = DateTime.Now
            };
            var userCreateResult = await _userManager.CreateAsync(appUser, password);

            if (!userCreateResult.Succeeded)
            {
                Log.Error("Account was not created");
                return;
            }
            Log.Information("User record created with login {userLogin} and password {userPass}", userName, password);

            var createdUser = await this._userService.GetUserByLogin(userName);
            appUser.UserId = createdUser.UserId;
            appUser.Guid = createdUser.Guid;
            foreach(var role in rolesList)
            {
                await this._userManager.AddToRoleAsync(appUser, role);
                Log.Information("User added to role {roleName}", role);
            }                        
        }

        public async Task ChangePass(string userName, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            Log.Information("TRY: Change user '{user}' pass to '{newPassword}'", userName, newPassword);

            var res = await this._userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!(res.Succeeded))
            {
                Log.Error("Password was not changed due to:");
                Log.Error("{@res}", res.Errors);
            }
            Log.Information("SUCCESS: Password changed successfully");
        }
    }    
}
