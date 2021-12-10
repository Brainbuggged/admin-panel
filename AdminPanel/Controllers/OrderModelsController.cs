using AdminPanel.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models.Models.NSI_Order;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class OrderModelsController : Controller
    {
        private readonly OnlineShopContext _context;

        public OrderModelsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: OrderModelsController
        public async Task<IActionResult> Index()
        {
            return View(await _context.orders.Include(c => c.vendor).Include(c => c.client).Include(x => x.products).Include(x => x.status_history).ToListAsync());
        }

        // GET: OrderModelsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderModelsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderModelsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderModelsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public JsonResult GetOrderProductModels(Guid orderModelId)
        {
            List<OrderProductModel> list = new List<OrderProductModel>();

            var model = _context.order_products.Where(x => x.orderid == orderModelId).AsNoTracking();

            foreach (var item in model)
            {
                list.Add(item);
            }

            return Json(list);
        }


        public JsonResult GetStatusChangeModel(Guid orderModelId)
        {
            List<OrderStatusChangeModelView> list = new List<OrderStatusChangeModelView>();

            var model = _context.order_status_changes.Where(x => x.orderid == orderModelId).AsNoTracking();

            foreach (var item in model)
            {
                list.Add(new OrderStatusChangeModelView { old_status = item.old_status.GetText(), new_status = item.new_status.GetText(),date = item.date });
            }

            return Json(list);
        }

        // GET: OrderModelsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderModelsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
