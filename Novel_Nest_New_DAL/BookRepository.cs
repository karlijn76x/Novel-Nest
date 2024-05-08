using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Models;
using MySqlConnector;
using static System.Reflection.Metadata.BlobBuilder;

namespace Novel_Nest_DAL
{
    public class BookRepository : IBookRepository
    {
        private readonly MyDbContext _DbContext;

        public BookRepository(MyDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<bool> AddBookAsync(BookDTO book)
        {
            try
            {
                using (var connection = _DbContext.OpenConnection())
                {
                    var query = "INSERT INTO book (Title, Author, CategoryId) VALUES (@Title, @Author, @CategoryId)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@title", book.Title);
                        command.Parameters.AddWithValue("@author", book.Author);
                        command.Parameters.AddWithValue("@categoryId", book.CategoryId);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
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
                return new List<CategoryDTO>(); 
            }
        }

		public List<BookDTO> GetBooks()
		{
			try
			{
				var books = new List<BookDTO>();

				using (var connection = _DbContext.OpenConnection())
				{
					var query = @"
                SELECT b.*, c.Name AS CategoryName 
                FROM book b 
                JOIN category c ON b.CategoryId = c.Id";
					using (var command = new MySqlCommand(query, connection))
					{
						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var book = new BookDTO
								{
									Id = reader.GetInt32("Id"),
									Title = reader.GetString("Title"),
									Author = reader.GetString("Author"),
									CategoryId = reader.GetInt32("CategoryId"),
									CategoryName = reader.GetString("CategoryName")
								};
								books.Add(book);
							}
						}
					}
				}
				return books;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving books: {ex.Message}");
				return new List<BookDTO>();
			}
		}


		public async Task<bool> DeleteBookAsync(int Id)
        {
            try
            {
                using (var connection = _DbContext.OpenConnection())
                {
                    var query = "DELETE FROM book WHERE Id = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book: {ex.Message}");
                return false;
            }

        }

        public async Task<bool> EditBookAsync(BookDTO book)
        {
            try
            {
                using (var connection = _DbContext.OpenConnection())
                {
                    var query = "UPDATE book SET Title = @Title, Author = @Author, CategoryId = @CategoryId WHERE Id = @BookId";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Author", book.Author);
                        command.Parameters.AddWithValue("@Title", book.Title);
                        command.Parameters.AddWithValue("@categoryId", book.CategoryId);
                        command.Parameters.AddWithValue("@BookId", book.Id);

                        await command.ExecuteNonQueryAsync();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book: {ex.Message}");
                return false;
            }
		}

		public string GetCategoryName(int bookId)
		{
			try
			{
				using (var connection = _DbContext.OpenConnection())
				{
					var query = "SELECT b.*, c.Name AS CategoryName FROM book b JOIN Category c ON b.CategoryId = c.Id WHERE b.Id = @bookId";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@BookId", bookId);
 

						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								return reader.GetString("CategoryName");
							}
						}
					}
				}
				return string.Empty; 
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving category name: {ex.Message}");
				return string.Empty;
			}
		}


	}
}
