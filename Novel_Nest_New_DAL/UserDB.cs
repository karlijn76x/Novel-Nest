using MySqlConnector;
using Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Novel_Nest_DAL
{
	public class UserDB : IUserDB
	{
		private readonly string _connectionString;
		

		public UserDB(string connectionString)
		{
			_connectionString = connectionString;
			
		}

        public async Task<bool> CreateUserAsync(UserModelDTO user)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "INSERT INTO user (Name, Age, Email, Password) VALUES (@Name, @Age, @Email, @Password)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Age", user.Age);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        // Hash het wachtwoord voordat het wordt opgeslagen
                        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                        command.Parameters.AddWithValue("@Password", hashedPassword);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log de uitzondering of handel deze af
                return false;
            }
        }

        public async Task<(bool isAuthenticated, string Name, int Id)> AuthenticateUserAsync(string email, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "SELECT Id, Name, Password FROM user WHERE Email = @Email";
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

                                // Verifieer het gehashte wachtwoord
                                if (BCrypt.Net.BCrypt.Verify(password, storedPassword))
                                {
                                    return (true, storedUserName, storedUserId);
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
            return (false, "", -1);
        }

    }
}
