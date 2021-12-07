using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Models.Par_Models
{
	public class ParameterValueModel
	{
		[Display(Name = "Идентификатор")]

		public Guid id { get; set; }
		[Display(Name = "Значение")]

		public string value { get; set; }
		[Display(Name = "Параметр айди")]

		public Guid parameterid { get; set; }

		[Display(Name = "Параметр")]
		public ParameterModel parameter { get; set; }
	}
}
