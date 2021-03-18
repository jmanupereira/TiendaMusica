using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaMusica.Data;
using TiendaMusica.Models;

namespace TiendaMusica.Controllers
{
    public class OtrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OtrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Otros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Otro.ToListAsync());
        }

        // GET: Otros/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otro = await _context.Otro
                .FirstOrDefaultAsync(m => m.ID == id);
            if (otro == null)
            {
                return NotFound();
            }

            return View(otro);
        }

        // GET: Otros/Create
        [Authorize(Roles="Admin, Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Otros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Marca,Modelo,Descripcion,Precio,Stock")] Otro otro)
        {
            if (ModelState.IsValid)
            {
                otro.ID = Guid.NewGuid();
                _context.Add(otro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(otro);
        }

        // GET: Otros/Edit/5
        [Authorize(Roles="Admin, Empleado")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otro = await _context.Otro.FindAsync(id);
            if (otro == null)
            {
                return NotFound();
            }
            return View(otro);
        }

        // POST: Otros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Marca,Modelo,Descripcion,Precio,Stock")] Otro otro)
        {
            if (id != otro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtroExists(otro.ID))
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
            return View(otro);
        }

        // GET: Otros/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otro = await _context.Otro
                .FirstOrDefaultAsync(m => m.ID == id);
            if (otro == null)
            {
                return NotFound();
            }

            return View(otro);
        }

        // POST: Otros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var otro = await _context.Otro.FindAsync(id);
            _context.Otro.Remove(otro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtroExists(Guid id)
        {
            return _context.Otro.Any(e => e.ID == id);
        }
    }
}
