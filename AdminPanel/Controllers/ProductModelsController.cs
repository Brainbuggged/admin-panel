﻿using System;
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
        public  IActionResult Index(bool is_checked)
        {
            var onlineShopContext = new List<ProductModel>();
            if (is_checked)
            {
                onlineShopContext = _context.products.Include(c => c.vendor).Include(c => c.category).Include(p => p.photoes).ToList();
            }
            else
            {
                onlineShopContext = _context.products.Where(x => x.status == Models.ProductStatus.OjidaetProverki).Include(c => c.vendor).Include(c => c.category).Include(p => p.photoes).ToList();

            }
            ViewData["awaiters"] = !is_checked;
            return View( onlineShopContext);
        }

        public async Task<IActionResult> IndexAwaitsModifier()
        {
            var onlineShopContext = _context.products.Where(p => p.is_checked).Include(c => c.vendor);
            return View(await onlineShopContext.ToListAsync());
        }

        public async Task<IActionResult> IndexAwaitsAdding()
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
        public async Task<IActionResult> Create([Bind("id,country,city,release_year,type,prise,count,is_delivery_expected,description,address,status,is_pickuped,is_delivered,x,y")] ProductModel productModel)
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

        public async Task<IActionResult> Decline(Guid? id)
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
            productModel.status = Models.ProductStatus.Archive;
            _context.Update(productModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Approve(Guid? id)
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
            productModel.status = Models.ProductStatus.Vistavlen;
            _context.Update(productModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: ClientModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id, country, city, release_year, type, prise, count, is_delivery_expected, description, address, status, is_pickuped, is_delivered, x, y")] ProductModel productModel)
        {
            if (id != productModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _context.products.Where(x => x.id == productModel.id).First();

                    product.country = productModel.country;
                    product.city = productModel.city;
                    product.release_year = productModel.release_year;
                    product.type = productModel.type;
                    product.prise = productModel.prise;
                    product.count = productModel.count;
                    product.is_delivery_expected = productModel.is_delivery_expected;
                    product.description = productModel.description;
                    product.address = productModel.address;
                    product.status = productModel.status;
                    product.is_pickuped = productModel.is_pickuped;
                    product.is_delivered = productModel.is_delivered;
                    product.x = productModel.x;
                    product.y = productModel.y;

                    _context.products.Update(product);
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
