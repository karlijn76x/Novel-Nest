
using Interfaces;

namespace Novel_Nest_Core
{
    public class UserService
    {
        private readonly IUserDB _userDB;

        public UserService(IUserDB userDB)
        {
            _userDB = userDB;
        }
        public async Task<(bool isAuthenticated, string Name, int Id)> AuthenticateUserAsync(string email, string password)
        {
            return await _userDB.AuthenticateUserAsync(email, password);
        }

        public async Task<bool> CreateUserAsync(UserModelDTO user)
        {
            return await _userDB.CreateUserAsync(user);
        }
    }
}
