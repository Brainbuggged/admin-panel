using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AdminPanel.Core.Repositories.NSI_Client;
using AdminPanel.Core.Repositories.NSI_Logger;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Vendors()
        {
            return View();
        }

        public async Task<IActionResult> Clients()
        {
            ClientRepository rep = new ClientRepository();
            var list =  await rep.GetAll();

            return View(list);
        }

        //public async Task<IActionResult> Products()
        //{
        //    ProductCategoryRepository rep = new ProductCategoryRepository();
        //    var list =  await rep.GetAll();

        //    return View(list);
        //}

        public async Task<IActionResult> Logs()
        {
            WebLogRepository rep = new WebLogRepository();
            var list = await rep.GetAll();

            return View(list);
        }

        public IActionResult Categories()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}