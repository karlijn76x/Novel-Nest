using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
	public interface IUserDB
	{
		Task<bool> CreateUserAsync(UserModelDTO user);
		Task<(bool isAuthenticated, string Name, int Id)> AuthenticateUserAsync(string email, string password);

    }
}
