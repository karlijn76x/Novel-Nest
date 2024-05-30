using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novel_Nest_DAL;


namespace Novel_Nest_Core
{
    public class UserService
    {
        private UserDB userDB;

        public UserService()
        {
            userDB = new UserDB(new MyDbContext("Server=127.0.0.1;Database=Novel_Nest_Db;Uid=root;"));
        }

        public async Task<(bool isAuthenticated, string Name, int Id)> AuthenticateUser(string email, string password)
        {
            return await userDB.AuthenticateUser(email, password);
        }

        public void CreateUser(UserModelDTO user)
        {
            userDB.CreateUserAsync(user);
        }
    }
}
