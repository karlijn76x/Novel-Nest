﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MySqlConnector;

namespace Novel_Nest_DAL
{
    public class BookRepository
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
                return new List<CategoryDTO>(); // Lege lijst retourneren in geval van fout
            }
        }

        public List<BookDTO> GetBooks()
        {
            try
            {
                var books = new List<BookDTO>();

                using (var connection = _DbContext.OpenConnection())
                {
                    var query = "SELECT * FROM book";
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
                                    CategoryId = reader.GetInt32("CategoryId")
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
                return new List<BookDTO>(); // Return empty list in case of error
            }
        }

    }
}
