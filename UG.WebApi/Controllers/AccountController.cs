using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using Serilog;
using System.Linq;

using AspNetCore.Identity.MySQLDapper.Model;
using UG.ORM;
using UG.WebApi.Auth;
using UG.WebApi.Models;

namespace UG.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]    
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._userService = userService;
        }

        /// <summary>
        /// Get claims list for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<IList<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            // Fill roles claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToLower()));
            }

            return claims;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Login);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);                    
                    if (result.Succeeded)
                    {
                        var claims = await GetUserClaims(user);

                        var key = SecurityAlgorithmService.signingKey;
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken("AltayIssuer",
                          "AltayAudience",
                          claims,
                          expires: DateTime.Now.AddMonths(1),
                          signingCredentials: creds
                        );

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), status = "success" });
                    }
                    else
                        Log.Warning("User {@userLogin} password is not correct!", model.Login);
                }
                else
                    Log.Warning("User {@userLogin} not found!", model.Login);
            }
            else
            {
                var msg = ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage);
                Log.Warning("Generate token try: model is not valid model: {@msg}", msg);
            }

            var res = new JsonResult("Неверное имя пользователя или пароль");
            return BadRequest(res);
        }
    }
}
