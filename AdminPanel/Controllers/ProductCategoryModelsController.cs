using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPanel.DataAccessLayer;
using AdminPanel.Models.Models.NSI_Product;
using AdminPanel.Models.Models.Par_Models;
using AdminPanel.ViewModels.Category.CreateAdminCategory;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;
using AdminPanel.Extensions;

namespace AdminPanel.Controllers
{
    public class ProductCategoryModelsController : Controller
    {
        private readonly OnlineShopContext _context;
        private readonly ParDBContext _parcontext;

        public ProductCategoryModelsController(OnlineShopContext context, ParDBContext parContext)
        {
            _context = context;
            _parcontext = parContext;
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

        // GET: ProductCategoryModels/AddCategory
        public IActionResult AddCategory(Guid? id)
        {
            //var parentList = _context.product_categories.Where(x => x.id == id);
            //ViewData["parentid"] = new SelectList(parentList, "id", "ru_name");
            return View();
        }
      
        // GET: ProductCategoryModels/Create
        public IActionResult Create()
        {
            var parentList = new List<ProductCategoryModel>();
            parentList.Add(new ProductCategoryModel() { id = Guid.Empty });
            parentList.AddRange(_context.product_categories);
            ViewData["parentid"] = new SelectList(parentList, "id", "ru_name");

            return View();
        }

        // POST: ProductCategoryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,parentid,ru_name,en_name,is_last,photo")] ProductCategoryModel productCategoryModel)
        {
            if (Guid.Parse(productCategoryModel.parentid) == Guid.Empty)
            {
                productCategoryModel.parentid = string.Empty;
            }
            if (productCategoryModel.is_last == null)
            {
                productCategoryModel.is_last = true;
            }
            if (ModelState.IsValid)
            {
                productCategoryModel.id = Guid.NewGuid();
                productCategoryModel.en_name = new TranslitExtension().Run(productCategoryModel.ru_name);
                _context.Add(productCategoryModel);

                CategoryModel catModel = new CategoryModel() { id = productCategoryModel.id, en_name = new TranslitExtension().Run(productCategoryModel.ru_name), name = productCategoryModel.ru_name };

                _parcontext.Add(catModel);

                await _context.SaveChangesAsync();
                await _parcontext.SaveChangesAsync();

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
            ViewData["parentid"] = new SelectList(_context.product_categories.Where(x => x.parentid != null && x.parentid != string.Empty), "id", "ru_name", productCategoryModel.parentid);

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
                    CategoryModel catModel = _parcontext.categories.Where(x => x.id == productCategoryModel.id).First();
                    catModel.en_name = productCategoryModel.en_name;
                    catModel.name = productCategoryModel.ru_name;
                    _context.Update(productCategoryModel);
                    await _context.SaveChangesAsync();

                    _parcontext.Update(catModel);
                    await _parcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
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

            var catModel = await _parcontext.categories.FindAsync(id);
            _parcontext.categories.Remove(catModel);
            await _parcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryModelExists(Guid id)
        {
            return _context.product_categories.Any(e => e.id == id);
        }
    }
}
