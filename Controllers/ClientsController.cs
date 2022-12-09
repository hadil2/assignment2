﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Lab4.Models.ViewModels;

namespace Lab4.Controllers
{
    public class ClientsController : Controller
    {
        private readonly MarketDbContext _context;

        public ClientsController(MarketDbContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(int? ID)
        {
            var viewModel = new BrokeragesViewModel
            {
                Clients = await _context.Clients
                .Include(n => n.subscriptions)
                .AsNoTracking()
                .OrderBy(i=>i.LastName)
                .ToListAsync(),
                Brokerages = await _context.Brokerages.Include(n => n.subscriptions).AsNoTracking().ToListAsync()
            };

            if (ID != null)
            {
                ViewData["ClientID"] = ID;
                

                viewModel.Subscriptions = viewModel.Clients.Where(
                   x=>x.ID == ID ).Single().subscriptions;

               
            }
            return View(viewModel);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,BirthDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,BirthDate")] Client client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ID))
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
            return View(client);
        }

        
        [Route("EditSubscriptions/{id}")]
        public async Task<IActionResult> EditSubscriptions(int id)
        {
            var viewModel = new BrokeragesViewModel
            {
                Clients = await _context.Clients
                .Include(n => n.subscriptions)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync(),
                Brokerages = await _context.Brokerages.Include(n => n.subscriptions).AsNoTracking().ToListAsync()
            };

            if (id != null)
            {
                ViewData["ClientID"] = id;


                viewModel.Subscriptions = viewModel.Clients.Where(
                   x => x.ID == id).Single().subscriptions;


            }

            return View(viewModel);
		}

        [HttpGet, ActionName("AddSubscriptions")]
        [Route("AddSubscriptions/{id1}/{id2}")]
        public async Task<IActionResult> AddSubscriptions(int id1, string id2)
        {
            Subscription subscription = new Subscription {ClientId=id1,BrokerageId=id2 };

            _context.Subscriptions.Add(subscription);

            await _context.SaveChangesAsync();

            var viewModel = new BrokeragesViewModel
            {
                Clients = await _context.Clients
                .Include(n => n.subscriptions)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync(),
                Brokerages = await _context.Brokerages.Include(n => n.subscriptions).AsNoTracking().ToListAsync()
            };

            if (id1 != null)
            {
                ViewData["ClientID"] = id1;


                viewModel.Subscriptions = viewModel.Clients.Where(
                   x => x.ID == id1).Single().subscriptions;


            }

            return View(nameof(EditSubscriptions), viewModel);
        }

        [HttpGet, ActionName("RemoveSubscriptions")]
        [Route("RemoveSubscriptions/{id1}/{id2}")]
        public async Task<IActionResult> RemoveSubscriptions(int id1, string id2)
        {

           Subscription subscription = _context.Subscriptions.Find(id1, id2);

            _context.Subscriptions.Remove(subscription);

            await _context.SaveChangesAsync();

            var viewModel = new BrokeragesViewModel
            {
                Clients = await _context.Clients
                .Include(n => n.subscriptions)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync(),
                Brokerages = await _context.Brokerages.Include(n => n.subscriptions).AsNoTracking().ToListAsync()
            };

            if (id1 != null)
            {
                ViewData["ClientID"] = id1;


                viewModel.Subscriptions = viewModel.Clients.Where(
                   x => x.ID == id1).Single().subscriptions;


            }

            return View(nameof(EditSubscriptions),viewModel);
        }




        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'MarketDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}
