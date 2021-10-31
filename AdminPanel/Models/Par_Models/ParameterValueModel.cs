using System;

namespace AdminPanel.Models.Models.Par_Models
{
	public class ParameterValueModel
	{
		public Guid id { get; set; }
		public string value { get; set; }
		public Guid parameterid { get; set; }
		public ParameterModel parameter { get; set; }
	}
}
