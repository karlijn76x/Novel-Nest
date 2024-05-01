using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Novel_Nest_DTO;


namespace Novel_Nest_DAL;

public class UserDB : IUserDB
{

    private readonly MyDbContext _DbContext;

    public UserDB(MyDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<bool> CreateUserAsync(UserModel user)
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
                    command.Parameters.AddWithValue("@Password", user.Password);

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
}


