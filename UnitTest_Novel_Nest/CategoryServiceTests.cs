using Models;
using Novel_Nest_Core;
using Moq;
using Interfaces;

namespace UnitTest_Novel_Nest
{
	[TestClass]
	public class CategoryServiceTests
	{
		private readonly Mock<ICategoryRepository> _mockCategoryRepository;
		private readonly CategoryService _categoryService;

		public CategoryServiceTests()
		{
			_mockCategoryRepository = new Mock<ICategoryRepository>();
			_categoryService = new CategoryService(_mockCategoryRepository.Object);
		}
	}
}
