using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPanel.DataAccessLayer;
using AdminPanel.Models.Models.NSI_Client;
using AdminPanel.Models;

namespace AdminPanel.Controllers
{
    public class ClientModelsController : Controller
    {
        private readonly OnlineShopContext _context;

        public ClientModelsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: ClientModels
        public async Task<IActionResult> Index()
        {
            var onlineShopContext = _context.clients.Include(c => c.vendor);
            return View(await onlineShopContext.ToListAsync());
        }

        // GET: ClientModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.clients
                .Include(c => c.vendor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return View(clientModel);
        }

        // GET: ClientModels/Create
        public IActionResult Create()
        {
            ViewData["vendorid"] = new SelectList(_context.vendors, "id", "id");
            return View();
        }

        // POST: ClientModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,photo,number,name,surname,patronymic,phone,balance,card_number,card_date,cvv,login,password,email,role,vendorid")] ClientModel clientModel)
        {
            if (ModelState.IsValid)
            {
                clientModel.id = Guid.NewGuid();
                _context.Add(clientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["vendorid"] = new SelectList(_context.vendors, "id", "id", clientModel.vendorid);
            return View(clientModel);
        }

        // GET: ClientModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.clients.FindAsync(id);
            if (clientModel == null)
            {
                return NotFound();
            }
            var dictionary = new Dictionary<string, string>();
            var vendors  = new Dictionary<Guid, string>();
            //foreach(var vendor in _context.vendors)
            //{
            //    vendors.Add(vendor.id, vendor.surname);
            //}

            ViewData["vendorid"] = new SelectList(_context.vendors, "id", "surname", clientModel.vendorid);

            return View(clientModel);
        }

        // POST: ClientModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,photo,number,name,surname,patronymic,phone,balance,card_number,card_date,cvv,login,password,email,role,vendorid")] ClientModel clientModel)
        {
            if (id != clientModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientModelExists(clientModel.id))
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
            ViewData["vendorid"] = new SelectList(_context.vendors, "id", "id", clientModel.vendorid);
            return View(clientModel);
        }

        // GET: ClientModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.clients
                .Include(c => c.vendor)
                .FirstOrDefaultAsync(m => m.id == id);
            if (clientModel == null)
            {
                return NotFound();
            }

            return View(clientModel);
        }

        // POST: ClientModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var clientModel = await _context.clients.FindAsync(id);
            _context.clients.Remove(clientModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientModelExists(Guid id)
        {
            return _context.clients.Any(e => e.id == id);
        }
    }
}
