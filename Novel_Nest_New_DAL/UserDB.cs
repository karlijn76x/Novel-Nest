using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Interfaces;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace Novel_Nest_DAL;

public class UserDB : IUserDB
{
    private readonly MyDbContext _DbContext;

    public UserDB(MyDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<bool> CreateUserAsync(UserModelDTO user)
    {
        try
        {
            using (var connection = _DbContext.OpenConnection())
            {
                var query = "INSERT INTO user (Name, Age, Email, Password) VALUES (@Name, @Age, @Email, @Password)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Age", user.Age);
                    command.Parameters.AddWithValue("@Email", user.Email);

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

    public async Task<(bool isAuthenticated, string Name, int Id)> AuthenticateUser(string email, string password)
    {
        try
        {
            using (var connection = _DbContext.OpenConnection())
            {
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

                            // Verifieer het ingevoerde wachtwoord met het opgeslagen gehashte wachtwoord
                            if (BCrypt.Net.BCrypt.Verify(password, storedHashedPassword))
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
            Console.WriteLine($"Error authenticating user: {ex.Message}");
        }
        return (false, null, -1);
    }


}


