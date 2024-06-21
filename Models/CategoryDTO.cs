
namespace Models
{
	public class CategoryDTO
	{
		public string Name { get; set; } = string.Empty;
		public int Id { get; set; } 
		public int? UserId { get; set; } 
		public string UserName { get; set; } = string.Empty;
		public bool IsDefault { get; set; }
	}
}
