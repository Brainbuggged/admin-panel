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
using AdminPanel.Extensions;
using AdminPanel.Models.Models.NSI_Vendor;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Text;
using System.IO;

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
            var vendorsList = new List<VendorModel>();
            vendorsList.Add(new VendorModel() { id = Guid.Empty });
            vendorsList.AddRange(_context.vendors);
            ViewData["vendorid"] = new SelectList(vendorsList, "id", "id");
            return View();
        }

       
        // POST: ClientModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,photo,number,name,surname,patronymic,phone,balance,card_number,card_date,cvv,login,password,email,role,vendorid,Image")] ClientViewModel clientModel)
        {
            //TODO: Photo upload
            if (ModelState.IsValid)
            {
                clientModel.id = Guid.NewGuid();
                clientModel.role = RoleType.Client;
                clientModel.vendor = null;
                clientModel.balance = 0;
                clientModel.number = new TranslitExtension().MakeName($"{clientModel.surname}{clientModel.name}{clientModel.patronymic}");
                clientModel.password = new EncrypterExtension().Encrypt(clientModel.password);

                string url = "http://77.73.67.101:93/api/Upload/upload-profile-photo";
                var request = (HttpWebRequest)WebRequest.Create(url);

                if (clientModel.Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        clientModel.Image.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                        var postData = clientModel.Image;
                        var data = fileBytes;

                        request.Method = "POST";
                        request.ContentType = "image";

                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                }


                var response = (HttpWebResponse)request.GetResponse();

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
            var vendorsList = new List<VendorModel>();
            vendorsList.Add(new VendorModel() { id = Guid.Empty });
            vendorsList.AddRange(_context.vendors);
            ViewData["vendorid"] = new SelectList(vendorsList, "id", "surname", clientModel.vendorid);

            return View(clientModel);
        }


        public async Task<IActionResult> Block(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.clients.FindAsync(id);
            clientModel.role = RoleType.Blocked;
            _context.Update(clientModel);

            var clientProducts = _context.products.Where(item => item.vendorid == clientModel.vendorid && item.status == ProductStatus.Vistavlen).ToList();

            clientProducts.ForEach(item => { item.status = ProductStatus.Snyat; });

            _context.products.UpdateRange(clientProducts);

            await _context.SaveChangesAsync();
            if (clientModel == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> UnBlock(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientModel = await _context.clients.FindAsync(id);
            if (clientModel.vendor == null)
            {
                clientModel.role = RoleType.Client;
            } 
            else
            {
                clientModel.role = RoleType.Seller;

            }

            _context.Update(clientModel);

            await _context.SaveChangesAsync();
            if (clientModel == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
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
                    Randomer rnd = new Randomer();
                    //берем сначал клиента
                    var client = _context.clients.SingleOrDefault(item => item.id == clientModel.id);
                    //потом немного апдейтим ДОПУСТИМЫЕ ПОЛЯ
                    client.photo = clientModel.photo;
                    client.phone = clientModel.phone;
                    client.card_number = clientModel.card_number;
                    client.card_date = clientModel.card_date;
                    client.cvv = clientModel.cvv;
                    client.login = clientModel.login;
                    client.email = clientModel.email;
                    //если роль не изменилась, то сохраняе
                    if (client.role == clientModel.role)
                        client.role = clientModel.role;
                    //исли роль изменилась на продавца
                    else if (clientModel.role == RoleType.Seller)
                    {
                        //если профиль продавца не был выбран
                        if (clientModel.vendorid == Guid.Empty || clientModel.vendorid == null)
                        {
                            var vendorId = Guid.NewGuid();
                            var vendor = new VendorModel
                            {
                                id = vendorId,
                                rating = 0,
                                phone = client.phone,
                                surname = client.surname,
                                name = client.name,
                                patronymic = client.patronymic,
                                is_fiz_face = false,
                                is_ur_face = true,
                                photo = client.photo,
                                socials = rnd.RandomSocials(vendorId),
                                is_assortment_updated = false,
                                work_phone = client.phone,
                                email = client.email,
                            };
                            _context.vendors.Add(vendor);
                            client.vendorid = vendorId;
                        }
                        //если профиль продавца был выбран
                        else
                            client.vendorid = clientModel.vendorid;

                        client.role = clientModel.role;
                    }
                    //если роль изменилась про покупателя\клиента
                    else if (clientModel.role == RoleType.Client)
                    {
                        //удаляем товары товары продавца из корзин
                        _context.cart_items.RemoveRange(_context.cart_items.Include(p => p.product).Where(item => item.product.vendorid == client.vendorid));
                        //снимаем все товары продавца с продажи
                        var clientProducts = _context.products.Where(item => item.vendorid == client.vendorid && item.status == ProductStatus.Vistavlen).ToList();
                        clientProducts.ForEach(item => { item.status = ProductStatus.Snyat; });
                        _context.products.UpdateRange(clientProducts);
                        //забираем профиль продавца
                        client.vendorid = null;
                        client.role = clientModel.role;
                    }
                    //если изменили с роли продавец на заморожено
                    else if (clientModel.role == RoleType.Blocked && client.role == RoleType.Seller)
                    {
                        //удаляем товары товары продавца из корзин
                        _context.cart_items.RemoveRange(_context.cart_items.Include(p => p.product).Where(item => item.product.vendorid == client.vendorid));
                        //снимаем все товары продавца с продажи
                        var clientProducts = _context.products.Where(item => item.vendorid == client.vendorid && item.status == ProductStatus.Vistavlen).ToList();
                        clientProducts.ForEach(item => { item.status = ProductStatus.Snyat; });
                        _context.products.UpdateRange(clientProducts);

                        client.role = clientModel.role;
                    }
                    //если изменили с роли клиент\покупатель на роль заморожено
                    else if (clientModel.role == RoleType.Blocked && client.role == RoleType.Client)
                        client.role = clientModel.role;
                    //апдейтим клиента
                    _context.clients.Update(client);

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
