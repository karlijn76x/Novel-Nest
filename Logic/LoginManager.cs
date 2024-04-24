using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Novel_Nest_Core
{
    public class LoginManager
{
    public bool CreateAccount(string email, string password, string name)
    {
        User user = new(email, name);
        PasswordHasher<User> passwordHasher = new();
        string hashedPassword = passwordHasher.HashPassword(user, password);
    }
  
}
}
