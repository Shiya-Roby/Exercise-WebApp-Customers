using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExerciseWebAppCustomers.Data;
using ExerciseWebAppCustomers.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ExerciseWebAppCustomers.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersDBContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CustomersController(CustomersDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CId,CName,CCity,PicFile")] Customer customer)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(customer);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}


            if (ModelState.IsValid)
            {
                string rPath = webHostEnvironment.WebRootPath;
                string fName = Path.GetFileNameWithoutExtension(customer.PicFile.FileName);
                string ext = Path.GetExtension(customer.PicFile.FileName);
                customer.CPic = fName + ext;
                string path = rPath + "/Images/" + customer.CPic;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await customer.PicFile.CopyToAsync(fileStream);
                }
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CId,CName,CCity,PicFile")] Customer customer)
        {
            if (id != customer.CId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(customer);
                    //await _context.SaveChangesAsync();
                     string rPath = webHostEnvironment.WebRootPath;
                     string fName = Path.GetFileNameWithoutExtension(customer.PicFile.FileName);
                     string ext = Path.GetExtension(customer.PicFile.FileName);
                     customer.CPic = fName + ext;
                     string path = rPath + "/Images/" + customer.CPic;
                     using (var fileStream = new FileStream(path, FileMode.Create))
                     {
                          await customer.PicFile.CopyToAsync(fileStream);
                      }
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                    
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CId == id);
        }
    }
}
