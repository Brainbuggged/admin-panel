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
    public class ParameterValueModelsController : Controller
    {
        private readonly ParDBContext _parcontext;

        public ParameterValueModelsController(ParDBContext parContext)
        {
            _parcontext = parContext;
        }

        // GET: CategoryModelsController
        public async Task<IActionResult> Index()
        {
            return View(await _parcontext.parameter_values.Include(x => x.parameter).ThenInclude(x => x.category).ToListAsync());
        }

        // GET: CategoryModelsController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _parcontext.parameter_values
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
            var parameterList = _parcontext.parameters.ToList();
            ViewData["parameterid"] = new SelectList(parameterList, "id", "name");

            var categoryList = _parcontext.categories.ToList();
            ViewData["categoryid"] = new SelectList(categoryList, "id", "name");

            return View();
        }

        // POST: CategoryModelsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("value,parameterid")] ParameterValueModel parameterValueModel)
        {
            try
            {
                parameterValueModel.id = Guid.NewGuid();
                _parcontext.Add(parameterValueModel);
                  
                await _parcontext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetParameter(Guid id)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            //ProdCatItems
            var items = _parcontext.parameters.ToList().Where(p => p.categoryid == id);


            foreach (var item in items)
            {
                list.Add(new SelectListItem { Value = item.id.ToString(), Text = item.name });
            }

            return Json(list);
        }


        // GET: CategoryModelsController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parameterModel = await _parcontext.parameter_values.FindAsync(id);
            if (parameterModel == null)
            {
                return NotFound();
            }
            ViewData["parameterid"] = new SelectList(_parcontext.parameters, "id", "name", parameterModel.parameter);

            return View(parameterModel);
        }


        // POST: CategoryModelsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("value, parameterid")] ParameterValueModel parameterValueModel)
        {
            if (id != parameterValueModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _parcontext.Update(parameterValueModel);
                    await _parcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parameterValueModel);
        }

        // GET: CategoryModelsController/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategoryModel = await _parcontext.parameter_values.Include(x => x.parameter)
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
            var parameterValueModel = await _parcontext.parameter_values.FindAsync(id);
            _parcontext.parameter_values.Remove(parameterValueModel);
            await _parcontext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
