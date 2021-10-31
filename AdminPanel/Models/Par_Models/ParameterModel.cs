using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AdminPanel.Models.Models.Par_Models
{
	[Index("name")]
	public class ParameterModel
	{
		public Guid id { get; set; }
		public Guid categoryid { get; set; }
		public CategoryModel category { get; set; }
		public string name { get; set; }
		public List<ParameterValueModel> values { get; set; }
	}
}
