using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Lab4.Models.ViewModels;
using System.Net;
using System.Security.Cryptography;

namespace Lab4.Controllers
{
    public class BrokeragesController : Controller
    {
        private readonly MarketDbContext _context;
        private readonly IWebHostEnvironment webHost;

        public BrokeragesController(MarketDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            webHost = webHostEnvironment;
        }

        
        // GET: Brokerages
        public async Task<IActionResult> Index(string? ID)
        {
            var viewModel = new BrokeragesViewModel
            {
                Brokerages = await _context.Brokerages
                .Include(n => n.subscriptions)
                .AsNoTracking()
                .OrderBy(i => i.Title)
                .ToListAsync(),
                Clients = await _context.Clients.Include(n => n.subscriptions).AsNoTracking().ToListAsync()
            };

            if(ID != null)
            {
                ViewData["BrokerageID"] = ID;
                viewModel.Subscriptions = viewModel.Brokerages.Where(
                    x => x.Id == ID).Single().subscriptions;
            }
            

            return View(viewModel);
        }

        public async Task<IActionResult> Advertisements(string ID)
        {
            AdsViewModel viewModel = new AdsViewModel
            {
                Brokerages = await _context.Brokerages.FindAsync(ID),
                Advertisements = await _context.Advertisements.Include(n => n.Brokerage).AsNoTracking().ToListAsync(),
            };


            return View(viewModel);
        }

        public IActionResult UploadAdvertisements( string ID)
        {
            Brokerage brokerage = _context.Brokerages.Where(x => x.Id == ID).Single();
            var oldBrokerID = new FileInputViewModel
            {
                OldBrokerID = brokerage.Id,
                BrokerName = brokerage.Title,
            };
            ViewData["BrokerageID"] = new SelectList(_context.Brokerages, "Id", "Id");
            return View(oldBrokerID);
        }
        [HttpPost]
        [Route("{id1}")]
        public IActionResult UploadAdvertisements(string id1, FileInputViewModel ve)
        {
            string fileName = UploadFile(ve);
            var ads = new Advertisement
            {
                fileName = ve.File.FileName,
                Image = fileName,
                BrokerageID = id1,
            };
            ViewData["BrokerageID"] = new SelectList(_context.Brokerages, "Id", "Id", ads.BrokerageID);
            _context.Advertisements.Add(ads);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private string UploadFile(FileInputViewModel ve)
        {
            string fileName = null;
            if(ve.File!=null)
            {
                string uploadDir = Path.Combine(webHost.WebRootPath, "Uploads");
                fileName = Guid.NewGuid().ToString()+"-"+ve.File.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ve.File.CopyTo(fileStream);
                }
            }
            return fileName;

        }
        
        public IActionResult DeleteAdvertisements(int ID)
        {
            

            if (ID == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            Advertisement advertisement = _context.Advertisements.Where(m => m.ID == ID).Single();
            if (advertisement == null)
            {
                return NotFound();
            }
            advertisement.BrokerTitle = _context.Brokerages.Where(x => x.Id == advertisement.BrokerageID).Single().Title;
            

            return View(advertisement);
        }

        [HttpPost, ActionName("DeleteAdvertisements")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdvertisementsConfirmed(int ID)
        {

            if (_context.Advertisements == null)
            {
                return Problem("Entity set 'MarketDbContext.Advertisements'  is null.");
            }
            Advertisement advertisement = _context.Advertisements.Where(m => m.ID == ID).Single();
            if (advertisement != null)
            {
                _context.Advertisements.Remove(advertisement);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Brokerages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Brokerages == null)
            {
                return NotFound();
            }

            var brokerage = await _context.Brokerages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brokerage == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: Brokerages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brokerages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Fee")] Brokerage brokerage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brokerage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brokerage);
        }

        // GET: Brokerages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Brokerages == null)
            {
                return NotFound();
            }

            var brokerage = await _context.Brokerages.FindAsync(id);
            
            if (brokerage == null)
            {
                return NotFound();
            }
            return View(brokerage);
        }

        // POST: Brokerages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Fee")] Brokerage brokerage)
        {
            if (id != brokerage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brokerage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrokerageExists(brokerage.Id))
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
            return View(brokerage);
        }

        // GET: Brokerages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Brokerages == null)
            {
                return NotFound();
            }

            var brokerage = await _context.Brokerages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (brokerage == null)
            {
                return NotFound();
            }

            return View(brokerage);
        }

        // POST: Brokerages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Brokerages == null)
            {
                return Problem("Entity set 'MarketDbContext.Brokerages'  is null.");
            }
            var brokerage = _context.Brokerages.Include(c=>c.advertisements).Where(x=>x.Id==id).Single();
            ICollection<Advertisement> advertisements = brokerage.advertisements;
            if (brokerage != null)
            {
                _context.Brokerages.Remove(brokerage);
                _context.Advertisements.RemoveRange(advertisements);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrokerageExists(string id)
        {
          return _context.Brokerages.Any(e => e.Id == id);
        }
    }
}
