using AdminPanel.Models.Models.Par_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.DataAccessLayer
{
	public class parDBObjects
	{
		public void Initial(ParDBContext _parContext)
		{
			if (_parContext.categories.Count() == 0)
			{
				var res = CreateParameters();
				res.ForEach(item =>
				{
					var category = new CategoryModel
					{
						id = Guid.NewGuid(),
						name = item.name,
						en_name = item.en_name
					};
					_parContext.categories.Add(category);


					item.parameters.ForEach(item2 =>
					{
						var parametr = new ParameterModel
						{
							id = Guid.NewGuid(),
							categoryid = category.id,
							name = item2.name,
							values = new List<ParameterValueModel>()
						};
						item2.values.ForEach(item3 =>
						{
							parametr.values.Add(new ParameterValueModel
							{
								id = Guid.NewGuid(),
								value = item3,
								parameterid = parametr.id
							});
						});

						_parContext.parameters.Add(parametr);
					});
				});
				_parContext.SaveChanges();
			}
		}
		public List<jsoner> CreateParameters()
		{
			var jsoner = new List<jsoner>();
			var path = AppContext.BaseDirectory + $"Data\\";
			using (StreamReader r = new StreamReader(Path.Combine(path, "categories-parameters.json")))
			{

				string json = r.ReadToEnd();
				byte[] bytes = Encoding.Default.GetBytes(json);
				json = Encoding.UTF8.GetString(bytes);
				jsoner = JsonConvert.DeserializeObject<List<jsoner>>(json);
			}
			return jsoner;
		}
	}
	public class jsoner
	{
		public string name { get; set; }
		public string en_name { get; set; }
		public List<Parameter> parameters { get; set; }
	}
	public class Parameter
	{
		public string name { get; set; }
		public List<string> values { get; set; }
	}
}
