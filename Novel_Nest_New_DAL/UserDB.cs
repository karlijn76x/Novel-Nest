using MySqlConnector;
using Interfaces;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

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

                        // Hash het wachtwoord voordat je het opslaat in de database
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                        command.Parameters.AddWithValue("@Password", hashedPassword);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
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
                                var storedUserName = reader.GetString("Name");
                                var storedHashedPassword = reader.GetString("Password");

                                // Print de opgehaalde gehashte wachtwoordwaarde naar de console
                                Console.WriteLine("Stored Hashed Password: " + storedHashedPassword);

                                // Controleer of het ingevoerde wachtwoord overeenkomt met het opgeslagen gehashte wachtwoord met BCrypt
                                var isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);

                                Console.WriteLine("Is Password Correct? " + isPasswordCorrect);

                                if (isPasswordCorrect)
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
                // Retourneer de fout naar de aanroepende methode in plaats van alleen een bericht naar de console te schrijven
                Console.WriteLine($"Error authenticating user: {ex.Message}");
                throw ex;
            }
            return (false, null, -1);
        }

    }
}
