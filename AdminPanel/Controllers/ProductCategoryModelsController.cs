using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPanel.DataAccessLayer;
using AdminPanel.Models.Models.NSI_Product;

namespace AdminPanel.Controllers
{
    public class ProductCategoryModelsController : Controller
    {
        private readonly OnlineShopContext _context;

        public ProductCategoryModelsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: ProductCategoryModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.product_categories.ToListAsync());
        }

        // GET: ProductCategoryModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
        public async Task<IActionResult> Create([Bind("id,parentid,ru_name,en_name,is_last,photo")] ProductCategoryModel productCategoryModel)
        {
            if (ModelState.IsValid)
            {
                productCategoryModel.id = Guid.NewGuid();
                _context.Add(productCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productCategoryModel);
        }

        // GET: ProductCategoryModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _context.product_categories.FindAsync(id);
            if (productCategoryModel == null)
            {
                return NotFound();
            }
            return View(productCategoryModel);
        }

        // POST: ProductCategoryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,parentid,ru_name,en_name,is_last,photo")] ProductCategoryModel productCategoryModel)
        {
            if (id != productCategoryModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryModelExists(productCategoryModel.id))
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
            return View(productCategoryModel);
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
            var productCategoryModel = await _context.product_categories.FindAsync(id);
            _context.product_categories.Remove(productCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryModelExists(Guid id)
        {
            return _context.product_categories.Any(e => e.id == id);
        }
    }
}
