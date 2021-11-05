using Microsoft.EntityFrameworkCore;
using System;

namespace AdminPanel.Models.Models.Par_Models
{
	[Index("name", "en_name")]
	public class CategoryModel
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string en_name { get; set; }
	}
}
