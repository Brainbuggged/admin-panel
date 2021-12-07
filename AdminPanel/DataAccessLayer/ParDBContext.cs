using AdminPanel.Models.Models.Par_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.DataAccessLayer
{
	public class ParDBContext : DbContext
	{
		public ParDBContext(DbContextOptions<ParDBContext> options) : base(options)
		{
			
		}

		public DbSet<CategoryModel> categories { get; set; }
		public DbSet<ParameterModel> parameters { get; set; }
		public DbSet<ParameterValueModel> parameter_values { get; set; }
	}
}
