using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MySqlConnector;

namespace Novel_Nest_DAL
{
	public class CategoryRepository
	{
		private readonly MyDbContext _DbContext;

		public CategoryRepository(MyDbContext dbContext)
		{
			_DbContext = dbContext;
		}
		public async Task<bool> AddCategoryAsync(CategoryDTO category)
		{
			try
			{
				using (var connection = _DbContext.OpenConnection())
				{
					var query = "INSERT INTO category (Name) VALUES (@Name)";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Name", category.Name);

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

        public List<CategoryDTO> GetCategories()
        {
            try
            {
                var categories = new List<CategoryDTO>();

                using (var connection = _DbContext.OpenConnection())
                {
                    var query = "SELECT * FROM category";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var category = new CategoryDTO
                                {
                                    Name = reader.GetString("Name") 
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

    }
}
