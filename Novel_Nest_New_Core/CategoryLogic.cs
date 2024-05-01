﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Novel_Nest_DAL;

namespace Novel_Nest_Core 
{ 

	public class CategoryLogic
	{
		private CategoryRepository categoryRepository;

		public CategoryLogic()
		{
			categoryRepository = new CategoryRepository(new MyDbContext("Server=127.0.0.1;Database=Novel_Nest_Db;Uid=root;"));
		}

		public void AddCategory(CategoryDTO category)
		{
			categoryRepository.AddCategoryAsync(category);
		}

		public List<CategoryDTO> GetCategories()
		{
			return categoryRepository.GetCategories();
		}
	}
}