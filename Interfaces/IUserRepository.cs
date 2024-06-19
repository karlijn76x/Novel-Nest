using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IUserRepository
	{
		public Task<string> GetUserNameByIdAsync(int userId);
		Task<bool> CreateUserAsync(UserModelDTO user);
		public Task<(bool isAuthenticated, string Name, int Id, string Role)> AuthenticateUserAsync(string email, string password);

    }
}
