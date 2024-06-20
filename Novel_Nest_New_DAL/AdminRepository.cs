using Interfaces;
using Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Novel_Nest_DAL
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesFromUsersAsync()
        {
            var categories = new List<CategoryDTO>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"
                SELECT c.Id, c.Name, c.UserId, u.Name AS UserName
                FROM category c
                JOIN user u ON c.UserId = u.Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categories.Add(new CategoryDTO
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name"),
                                    UserId = reader.GetInt32("UserId"),
                                    UserName = reader.GetString("UserName")
                                });
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all categories: {ex.Message}");
            }
            return categories;
        }

        public async Task<bool> DeleteCategoryFromUserAsync(int categoryId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "DELETE FROM category WHERE Id = @CategoryId";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@categoryId", categoryId);
                        var result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}");
                return false;
            }
        }

        // In AdminRepository.cs

        public async Task<List<BookDTO>> GetAllBooksAsync()
        {
            var books = new List<BookDTO>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = @"
            SELECT b.Id, b.Title, b.Author, c.Name AS CategoryName, u.Name AS UserName
            FROM book b
            JOIN category c ON b.CategoryId = c.Id
            JOIN user u ON b.UserId = u.Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                books.Add(new BookDTO
                                {
                                    Id = reader.GetInt32("Id"),
                                    Title = reader.GetString("Title"),
                                    Author = reader.GetString("Author"),
                                    CategoryName = reader.GetString("CategoryName"),
                                    UserName = reader.GetString("UserName")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all books: {ex.Message}");
            }
            return books;
        }

		public async Task<bool> EditBookAsync(BookDTO book)
		{
			try
			{
				using (var connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = @"
                UPDATE book
                SET Title = @Title, Author = @Author, CategoryId = @CategoryId
                WHERE Id = @Id AND UserId = @UserId";

					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Title", book.Title);
						command.Parameters.AddWithValue("@Author", book.Author);
						command.Parameters.AddWithValue("@CategoryId", book.CategoryId);
						command.Parameters.AddWithValue("@Id", book.Id);
						command.Parameters.AddWithValue("@UserId", book.UserId);

						var result = await command.ExecuteNonQueryAsync();
						return result > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error editing book: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> DeleteBookAsync(int bookId, int userId)
		{
			try
			{
				using (var connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = "DELETE FROM book WHERE Id = @BookId AND UserId = @UserId";

					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@BookId", bookId);
						command.Parameters.AddWithValue("@UserId", userId);
						var result = await command.ExecuteNonQueryAsync();
						return result > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting book: {ex.Message}");
				return false;
			}
		}


	}
}
