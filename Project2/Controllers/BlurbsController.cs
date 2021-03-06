﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Project2.Controllers
{
    [Authorize]
    public class BlurbsController : Controller
    {
        private readonly Project2Context _context;

        public BlurbsController(Project2Context context)
        {
            _context = context;
        }

        // GET: Blurbs
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var blurb1 = _context.Blurb.FirstOrDefault();
            if (blurb1 == null)
            {
                ViewData["Empty"] = "There are no Blurbs! yet... \nBe the First to create a Blurb!";
            }
            return View(await _context.Blurb.ToListAsync());
        }
        // GET: Blurbs/Userblurbs
        [AllowAnonymous]
        public async Task<IActionResult> Userblurbs(string id)
        {
            var firstBlurb =  _context.Blurb.First(blurb => blurb.TKeyID == id);
            ViewData["Title"] = firstBlurb.UserID;
            ViewData["nameHead"] = firstBlurb.UserID;
            return View(await _context.Blurb.Where(blurb => blurb.TKeyID == id).ToListAsync());
        }

        // GET: Blurbs/Blrub/5
        public async Task<IActionResult> Blurb(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Noblurb));
            }

            var blurb = await _context.Blurb
                .SingleOrDefaultAsync(m => m.BlurbID == id);
            if (blurb == null)
            {
                return RedirectToAction(nameof(Noblurb));
            }

            return View(blurb);
        }

        // GET: Blurbs/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Blurbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlurbID,Title,Body")] Blurb blurb)
        {
            blurb.Date = DateTime.Now;
            blurb.TKeyID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            blurb.UserID = User.FindFirstValue(ClaimTypes.Name);
            if (ModelState.IsValid)
            {
                _context.Add(blurb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blurb);
        }

        // GET: Blurbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Noblurb));
            }

            var blurb = await _context.Blurb.SingleOrDefaultAsync(m => m.BlurbID == id);
            if (blurb == null)
            {
                return RedirectToAction(nameof(Noblurb));
            }
            if (blurb.TKeyID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            return View(blurb);
        }

        // POST: Blurbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlurbID,Title,Body")] Blurb blurb)
        {
            blurb.Date = DateTime.Now;
            blurb.TKeyID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            blurb.UserID = User.FindFirstValue(ClaimTypes.Name);
            if (id != blurb.BlurbID)
            {
                return RedirectToAction(nameof(Noblurb));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blurb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlurbExists(blurb.BlurbID))
                    {
                        return RedirectToAction(nameof(Noblurb));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            if (blurb.TKeyID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            return View(blurb);
        }

        // GET: Blurbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Noblurb));
            }

            var blurb = await _context.Blurb
                .SingleOrDefaultAsync(m => m.BlurbID == id);
            if (blurb == null)
            {
                return RedirectToAction(nameof(Noblurb));
            }
            if (blurb.TKeyID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            return View(blurb);
        }


        // POST: Blurbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blurb = await _context.Blurb.SingleOrDefaultAsync(m => m.BlurbID == id);
            _context.Blurb.Remove(blurb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Blurbs/Reply
        public IActionResult Reply()
        {

            return View();
        }

        // POST: Blurbs/Reply
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(int? id, [Bind("BlurbID,Title,Body")] Blurb blurb )
        {
            blurb.Date = DateTime.Now;
            blurb.TKeyID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            blurb.UserID = User.FindFirstValue(ClaimTypes.Name);
            string itemPrev = id.ToString();
            blurb.ReplyID = itemPrev;
            if (ModelState.IsValid)
            {
                _context.Add(blurb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blurb);
        }

        private bool BlurbExists(int id)
        {
            return _context.Blurb.Any(e => e.BlurbID == id);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Noblurb()
        {
            return View();
        }
    }
}
