using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UG.WebApi.Models
{
    public class LoginModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ChangePasswordModel
    {
        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
