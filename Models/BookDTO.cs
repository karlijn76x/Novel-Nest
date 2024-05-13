using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class BookDTO
	{   
		public int Id {  get; set; }
		public string Title { get; set; } = string.Empty; 
		
		public string Author {  get; set; } = string.Empty ;
		public int CategoryId { get; set; }
		public string CategoryName { get; set; } = string.Empty;
		public DateTime ? DateStarted { get; set; }
		public DateTime? EndDate { get; set; }

	}
}
