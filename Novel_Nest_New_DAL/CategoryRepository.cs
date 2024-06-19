using Interfaces;
using Models;
using MySqlConnector;

namespace Novel_Nest_DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO category)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "INSERT INTO category (Name, UserId) VALUES (@Name, @UserId)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", category.Name);
                        command.Parameters.AddWithValue("@UserId", category.UserId); 
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating category: {ex.Message}");
                return false;
            }
        }

        public List<CategoryDTO> GetCategories(int userId)
        {
            try
            {
                var categories = new List<CategoryDTO>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT * FROM category WHERE UserId = @UserId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var category = new CategoryDTO
                                {
                                    Name = reader.GetString("Name"),
                                    Id = reader.GetInt32("Id"),
                                    UserId = userId // Zorg ervoor dat de UserId wordt ingesteld
                                };
                                categories.Add(category);
                            }
                        }
                    }
                }
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving categories: {ex.Message}");
                return new List<CategoryDTO>(); // Lege lijst retourneren in geval van fout
            }
        }

        public async Task<bool> DeleteCategoryAsync(int Id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "DELETE FROM category WHERE Id = @CategoryId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", Id);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EditCategoryAsync(CategoryDTO category)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    // Voeg de UserId controle toe in de WHERE-clausule om te verzekeren dat de categorie toebehoort aan de gebruiker
                    var query = "UPDATE category SET Name = @Name WHERE Id = @Id AND UserId = @UserId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", category.Name);
                        command.Parameters.AddWithValue("@Id", category.Id);
                        command.Parameters.AddWithValue("@UserId", category.UserId); // Voeg de UserId parameter toe
                        var result = await command.ExecuteNonQueryAsync();
                        // Controleer of er daadwerkelijk een rij is bijgewerkt
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating category: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsCategoryInUseAsync(int categoryId)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = "SELECT COUNT(1) FROM book WHERE CategoryId = @CategoryId";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@CategoryId", categoryId);
						var result = await command.ExecuteScalarAsync();
						return Convert.ToInt32(result) > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error checking if category is in use: {ex.Message}");
				return false;
			}
		}
	}
}
