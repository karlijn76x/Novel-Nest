using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Models;
using MySqlConnector;

namespace Novel_Nest_DAL
{
	public class CategoryRepository : ICategoryRepository
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
									Name = reader.GetString("Name"),
									Id = reader.GetInt32("Id")
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
				using (var connection = _DbContext.OpenConnection())
				{
					var query = "DELETE FROM category WHERE id = @CategoryId";
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
				Console.WriteLine($"Error with deleting category: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> EditCategoryAsync(CategoryDTO category)
		{
			try
			{
				using (var connection = _DbContext.OpenConnection())
				{
					var query = "UPDATE category SET Name = @Name WHERE Id = @Id";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Name", category.Name);
						command.Parameters.AddWithValue("@Id", category.Id);

						await command.ExecuteNonQueryAsync();
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating category: {ex.Message}");
				return false;
			}
		}

		
	}
}





