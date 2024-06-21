using MySqlConnector;
using Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Novel_Nest_DAL
{
	public class UserRepository : IUserRepository
	{
		private readonly string _connectionString;
		

		public UserRepository(string connectionString)
		{
			_connectionString = connectionString;
			
		}
		public async Task<string> GetUserNameByIdAsync(int userId)
		{
			await using var connection = new MySqlConnection(_connectionString);
			await connection.OpenAsync();
			using var command = new MySqlCommand("SELECT Name FROM user WHERE Id = @UserId", connection);
			command.Parameters.AddWithValue("@userId", userId);
			var result = await command.ExecuteScalarAsync();
			return result?.ToString();
		}

        public async Task<bool> CreateUserAsync(UserModel user)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "INSERT INTO user (Name, Age, Email, Password, Role) VALUES (@Name, @Age, @Email, @Password, @Role)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Age", user.Age);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@Role", user.Role); 

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public async Task<(bool isAuthenticated, string Name, int Id, string Role)> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT Id, Name, Password, Role FROM user WHERE Email = @Email";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var storedUserId = reader.GetInt32("Id");
                                var storedUserName = reader.IsDBNull(reader.GetOrdinal("Name")) ? "" : reader.GetString("Name");
                                var storedPassword = reader.GetString("Password");
                                var storedRole = reader.GetString("Role");

                                if (BCrypt.Net.BCrypt.Verify(password, storedPassword))
                                {
                                    return (true, storedUserName, storedUserId, storedRole);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log de uitzondering of handel deze af
                throw;
            }
            return (false, "", -1, "");
        }


    }
}
