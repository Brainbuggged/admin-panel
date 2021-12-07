using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPanel.DataAccessLayer;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.NSI_Vendor;

namespace AdminPanel.Controllers
{
    public class VendorModelsController : Controller
    {
        private readonly OnlineShopContext _context;

        public VendorModelsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: ProductCategoryModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.vendors.Include(x=> x.orders).ToListAsync());
        }

        // GET: ProductCategoryModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorModel = await _context.vendors
                .FirstOrDefaultAsync(m => m.id == id);
            if (vendorModel == null)
            {
                return NotFound();
            }

            return View(vendorModel);
        }

        // GET: ProductCategoryModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategoryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,parentid,ru_name,en_name,is_last,photo")] VendorModel vendorModel)
        {
            if (ModelState.IsValid)
            {
                vendorModel.id = Guid.NewGuid();
                _context.Add(vendorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorModel);
        }

        // GET: ProductCategoryModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorModel = await _context.product_categories.FindAsync(id);
            if (vendorModel == null)
            {
                return NotFound();
            }
            return View(vendorModel);
        }

        // POST: ProductCategoryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,parentid,ru_name,en_name,is_last,photo")] VendorModel vendorModel)
        {
            if (id != vendorModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryModelExists(vendorModel.id))
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
            return View(vendorModel);
        }

        // GET: ProductCategoryModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _context.product_categories
                .FirstOrDefaultAsync(m => m.id == id);
            if (productCategoryModel == null)
            {
                return NotFound();
            }

            return View(productCategoryModel);
        }

        // POST: ProductCategoryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vendorModel = await _context.vendors.FindAsync(id);
            _context.vendors.Remove(vendorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryModelExists(Guid id)
        {
            return _context.vendors.Any(e => e.id == id);
        }
    }
}
