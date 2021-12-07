using AdminPanel.DataAccessLayer;
using AdminPanel.Models.Models.Par_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Controllers
{
    public class ParameterModelsController : Controller
    {
        private readonly ParDBContext _parcontext;

        public ParameterModelsController(ParDBContext parContext)
        {
            _parcontext = parContext;
        }

        // GET: CategoryModelsController
        public async Task<IActionResult> Index()
        {
            return View(await _parcontext.parameters.Include(x => x.category).ToListAsync());
        }

        // GET: CategoryModelsController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _parcontext.parameters
                .FirstOrDefaultAsync(m => m.id == id);
            if (productCategoryModel == null)
            {
                return NotFound();
            }

            return View(productCategoryModel);
        }

        // GET: CategoryModelsController/Create
        public IActionResult Create()
        {
            var categoryList = _parcontext.categories.ToList();
          
            ViewData["categoryid"] = new SelectList(categoryList, "id", "name");
            return View();
        }

        // POST: CategoryModelsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,categoryid,name")] ParameterModel parameterModel)
        {
            try
            {
                parameterModel.id = Guid.NewGuid();
                _parcontext.Add(parameterModel);
                  
                await _parcontext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryModelsController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterModel = await _parcontext.parameters.FindAsync(id);
            if (parameterModel == null)
            {
                return NotFound();
            }
            ViewData["categoryid"] = new SelectList(_parcontext.categories, "id", "name", parameterModel.categoryid);

            return View(parameterModel);
        }


        // POST: CategoryModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,categoryid,name")] ParameterModel parameterModel)
        {
            if (id != parameterModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _parcontext.Update(parameterModel);
                    await _parcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parameterModel);
        }

        // GET: CategoryModelsController/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _parcontext.parameters.Include(x => x.category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (productCategoryModel == null)
            {
                return NotFound();
            }

            return View(productCategoryModel);
        }

        // POST: CategoryModelsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var parameterModel = await _parcontext.parameters.FindAsync(id);
            _parcontext.parameters.Remove(parameterModel);
            await _parcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
