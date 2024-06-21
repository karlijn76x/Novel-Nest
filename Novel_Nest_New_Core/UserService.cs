
using Interfaces;
using Novel_Nest_DAL;

namespace Novel_Nest_Core
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
		public async Task<string> GetUserNameAsync(int userId)
		{
			return await _userRepository.GetUserNameByIdAsync(userId);
		}
        public async Task<(bool isAuthenticated, string Name, int Id, string Role)> AuthenticateUserAsync(string email, string password)
        {
            return await _userRepository.AuthenticateUserAsync(email, password);
        }

        public async Task<bool> CreateUserAsync(UserModel user)
        {
            return await _userRepository.CreateUserAsync(user);
        }
    }
}
