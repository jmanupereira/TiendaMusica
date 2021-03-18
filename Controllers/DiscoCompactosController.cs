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
    public class DiscoCompactosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscoCompactosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiscoCompactos
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscoCompacto.ToListAsync());
        }

        // GET: DiscoCompactos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discoCompacto = await _context.DiscoCompacto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (discoCompacto == null)
            {
                return NotFound();
            }

            return View(discoCompacto);
        }

        // GET: DiscoCompactos/Create
        [Authorize(Roles="Admin, Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiscoCompactos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Sello,CodigoSello,Artista,Titulo,FechaPublicacion,Genero,Estilo,Descripcion,Precio,Stock")] DiscoCompacto discoCompacto)
        {
            if (ModelState.IsValid)
            {
                discoCompacto.ID = Guid.NewGuid();
                _context.Add(discoCompacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discoCompacto);
        }

        // GET: DiscoCompactos/Edit/5
        [Authorize(Roles="Admin, Empleado")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discoCompacto = await _context.DiscoCompacto.FindAsync(id);
            if (discoCompacto == null)
            {
                return NotFound();
            }
            return View(discoCompacto);
        }

        // POST: DiscoCompactos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Sello,CodigoSello,Artista,Titulo,FechaPublicacion,Genero,Estilo,Descripcion,Precio,Stock")] DiscoCompacto discoCompacto)
        {
            if (id != discoCompacto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discoCompacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscoCompactoExists(discoCompacto.ID))
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
            return View(discoCompacto);
        }

        // GET: DiscoCompactos/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discoCompacto = await _context.DiscoCompacto
                .FirstOrDefaultAsync(m => m.ID == id);
            if (discoCompacto == null)
            {
                return NotFound();
            }

            return View(discoCompacto);
        }

        // POST: DiscoCompactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var discoCompacto = await _context.DiscoCompacto.FindAsync(id);
            _context.DiscoCompacto.Remove(discoCompacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscoCompactoExists(Guid id)
        {
            return _context.DiscoCompacto.Any(e => e.ID == id);
        }
    }
}
