using Interfaces;
using Models;
using MySqlConnector;

namespace Novel_Nest_DAL
{
	public class BookRepository : IBookRepository
	{
		private readonly string _connectionString;

		public BookRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<bool> AddBookAsync(BookModel book)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = "INSERT INTO book (Title, Author, CategoryId, UserId) VALUES (@Title, @Author, @CategoryId, @UserId)";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Title", book.Title);
						command.Parameters.AddWithValue("@Author", book.Author);
						command.Parameters.AddWithValue("@CategoryId", book.CategoryId);
						command.Parameters.AddWithValue("@UserId", book.UserId); 

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

		public List<BookModel> GetBooks(int userId)
		{
			try
			{
				var books = new List<BookModel>();

				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					connection.Open();
					var query = @"
						SELECT b.*, c.Name AS CategoryName 
						FROM book b 
						JOIN category c ON b.CategoryId = c.Id
						WHERE b.UserId = @UserId"; 
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@UserId", userId); 
						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var book = new BookModel
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
				return new List<BookModel>();
			}
		}
		public async Task<BookModel> GetBookByUserIdAndBookId(int userId, int bookId)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = @"
							SELECT b.*, c.Name AS CategoryName, 
								   nb.DateStarted, nb.DateFinished, nb.Rating, nb.Review, nb.Finished
							FROM book b 
							JOIN category c ON b.CategoryId = c.Id
							LEFT JOIN nightstandbook nb ON b.Id = nb.BookId AND nb.UserId = @UserId
							WHERE b.UserId = @UserId AND b.Id = @BookId";

					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@UserId", userId);
						command.Parameters.AddWithValue("@BookId", bookId);

						using (var reader = await command.ExecuteReaderAsync())
						{
							if (reader.Read())
							{
								return new BookModel
								{
									Id = reader.GetInt32("Id"),
									Title = reader.GetString("Title"),
									Author = reader.GetString("Author"),
									CategoryId = reader.GetInt32("CategoryId"),
									CategoryName = reader.GetString("CategoryName"),
									DateStarted = reader.IsDBNull(reader.GetOrdinal("DateStarted")) ? null : (DateTime?)reader.GetDateTime("DateStarted"),
									DateFinished = reader.IsDBNull(reader.GetOrdinal("DateFinished")) ? null : (DateTime?)reader.GetDateTime("DateFinished"),
									Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? null : (int?)reader.GetInt32("Rating"),
									Review = reader.IsDBNull(reader.GetOrdinal("Review")) ? null : reader.GetString("Review"),
									Finished = reader.IsDBNull(reader.GetOrdinal("Finished")) ? false : reader.GetBoolean("Finished")
								};
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving book by UserId and BookId: {ex.Message}");
			}
			return null;
		}

		public async Task<bool> DeleteBookAsync(int Id)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
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

		public async Task<bool> EditLibraryBookAsync(BookModel book)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();

					var query = "UPDATE book SET Title = @Title, Author = @Author, CategoryId = @CategoryId WHERE Id = @Id AND UserId = @UserId";
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
				Console.WriteLine($"Error updating book: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> AddBookToNightstandAsync(NightstandBookModel nightstandBook)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = "INSERT INTO nightstandbook (BookId, DateStarted, UserId, Finished) VALUES (@BookId, @DateStarted, @UserId, @Finished)";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DateStarted", nightstandBook.DateStarted ?? (object)DBNull.Value);
						command.Parameters.AddWithValue("@BookId", nightstandBook.BookId);
						command.Parameters.AddWithValue("@UserId", nightstandBook.UserId);
						command.Parameters.AddWithValue("@Finished", nightstandBook.Finished); 

						await command.ExecuteNonQueryAsync();
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding book to nightstand: {ex.Message}");
				return false;
			}
		}

		public List<NightstandBookModel> GetNightstandBooks(int userId)
		{
			try
			{
				var nightstandBooks = new List<NightstandBookModel>();

				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					connection.Open();
					var query = @"
						SELECT nb.Id, nb.BookId, b.Title, b.Author, nb.DateStarted
						FROM nightstandbook nb
						JOIN book b ON b.Id = nb.BookId
						WHERE nb.UserId = @UserId";

					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@UserId", userId);

						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var nightstandBook = new NightstandBookModel
								{
									Id = reader.GetInt32("Id"),
									BookId = reader.GetInt32("BookId"),
									Title = reader.GetString("Title"),
									Author = reader.GetString("Author"),
									DateStarted = reader.GetDateTime("DateStarted"),
									UserId = userId
								};
								nightstandBooks.Add(nightstandBook);
							}
						}
					}
				}

				return nightstandBooks;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving nightstand books: {ex.Message}");
				return new List<NightstandBookModel>();
			}
		}

		public async Task<bool> DeleteNightstandBookAsync(int Id)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = "DELETE FROM nightstandbook WHERE Id = @Id";
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
		public async Task<bool> IsBookInNightstandAsync(int bookId)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = "SELECT COUNT(1) FROM nightstandbook WHERE BookId = @BookId";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@BookId", bookId);
						var result = await command.ExecuteScalarAsync();
						return Convert.ToInt32(result) > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error checking if book is in nightstand: {ex.Message}");
				return false;
			}
		}

		public async Task<NightstandBookModel> GetNightstandBookByUserIdAndBookId(int userId, int bookId)
		{
			try
			{
				using (var connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = @"
                SELECT nb.*, b.Title FROM nightstandbook nb
                JOIN book b ON nb.BookId = b.Id
                WHERE nb.UserId = @UserId AND nb.BookId = @BookId";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@UserId", userId);
						command.Parameters.AddWithValue("@BookId", bookId);
						using (var reader = await command.ExecuteReaderAsync())
						{
							if (reader.Read())
							{
								return new NightstandBookModel
								{
									Id = reader.GetInt32("Id"),
									BookId = reader.GetInt32("BookId"),
									UserId = reader.GetInt32("UserId"),
									Title = reader.GetString("Title"),
								};
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error retrieving nightstand book by UserId and BookId: {ex.Message}");
			}
			return null;
		}

		public async Task<bool> ReviewNightstandBookAsync(NightstandBookModel nightstandBook)
		{
			try
			{
				using (var connection = new MySqlConnection(_connectionString))
				{
					await connection.OpenAsync();
					var query = @"UPDATE nightstandbook 
                          SET Review = @Review, DateFinished = @DateFinished, Rating = @Rating, Finished = @Finished 
                          WHERE UserId = @UserId AND BookId = @BookId;";
					using (var command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Review", nightstandBook.Review ?? (object)DBNull.Value);
						command.Parameters.AddWithValue("@DateFinished", nightstandBook.DateFinished ?? (object)DBNull.Value);
						command.Parameters.AddWithValue("@Rating", nightstandBook.Rating ?? (object)DBNull.Value);
						command.Parameters.AddWithValue("@Finished", nightstandBook.Finished);
						command.Parameters.AddWithValue("@UserId", nightstandBook.UserId);
						command.Parameters.AddWithValue("@BookId", nightstandBook.BookId);

						var result = await command.ExecuteNonQueryAsync();
						return result > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating nightstand book review: {ex.Message}");
				return false;
			}
		}
	}
}
