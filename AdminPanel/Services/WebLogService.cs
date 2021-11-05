using System.Threading.Tasks;
using AdminPanel.Core.Repositories.NSI_Logger;
using AdminPanel.Models.Models.NSI_Logger;

namespace AdminPanel.Services
{
	public class WebLogService
	{
		public async Task AddLog(WebLogModel newLog)
		{
			await new WebLogRepository().AddAsync(newLog);
		}
	}
}
