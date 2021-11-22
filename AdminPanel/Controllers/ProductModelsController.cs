using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPanel.DataAccessLayer;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models.Models.NSI_Product;

namespace AdminPanel.Controllers
{
    public class ProductModelsController : Controller
    {
        private readonly OnlineShopContext _context;

        public ProductModelsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: ClientModels
        public async Task<IActionResult> Index()
        {
            var onlineShopContext = _context.products.Where(p => p.is_checked == false).Include(c => c.vendor);
            return View(await onlineShopContext.ToListAsync());
        }

        public async Task<IActionResult> IndexIsChecked()
        {
            var onlineShopContext = _context.products.Where(p => p.is_checked).Include(c => c.vendor);
            return View(await onlineShopContext.ToListAsync());
        }


        // GET: ClientModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.products
                .Include(c => c.vendor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: ClientModels/Create
        public IActionResult Create()
        {
            ViewData["vendorid"] = new SelectList(_context.products, "id", "id");
            return View();
        }

        // POST: ClientModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,photo,number,name,surname,patronymic,phone,balance,card_number,card_date,cvv,login,password,email,role,vendorid")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                productModel.id = Guid.NewGuid();
                _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["vendorid"] = new SelectList(_context.products, "id", "id", productModel.vendorid);
            return View(productModel);
        }

        // GET: ClientModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            ViewData["vendorid"] = new SelectList(_context.vendors, "id", "id", productModel.vendorid);
            return View(productModel);
        }

        // POST: ClientModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,photo,number,name,surname,patronymic,phone,balance,card_number,card_date,cvv,login,password,email,role,vendorid")] ProductModel productModel)
        {
            if (id != productModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientModelExists(productModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["vendorid"] = new SelectList(_context.vendors, "id", "id", productModel.vendorid);
            return View(productModel);
        }

        // GET: ClientModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.products
                .Include(c => c.vendor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: ClientModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productModel = await _context.products.FindAsync(id);
            _context.products.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientModelExists(Guid id)
        {
            return _context.products.Any(e => e.id == id);
        }
    }
}
