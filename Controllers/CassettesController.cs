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
    public class CassettesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CassettesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cassettes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cassette.ToListAsync());
        }

        // GET: Cassettes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cassette = await _context.Cassette
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cassette == null)
            {
                return NotFound();
            }

            return View(cassette);
        }

        // GET: Cassettes/Create
        [Authorize(Roles="Admin, Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cassettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Sello,CodigoSello,Artista,Titulo,FechaPublicacion,Genero,Estilo,Descripcion,Precio,Stock")] Cassette cassette)
        {
            if (ModelState.IsValid)
            {
                cassette.ID = Guid.NewGuid();
                _context.Add(cassette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cassette);
        }

        // GET: Cassettes/Edit/5
        [Authorize(Roles="Admin, Empleado")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cassette = await _context.Cassette.FindAsync(id);
            if (cassette == null)
            {
                return NotFound();
            }
            return View(cassette);
        }

        // POST: Cassettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Sello,CodigoSello,Artista,Titulo,FechaPublicacion,Genero,Estilo,Descripcion,Precio,Stock")] Cassette cassette)
        {
            if (id != cassette.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cassette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CassetteExists(cassette.ID))
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
            return View(cassette);
        }

        // GET: Cassettes/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cassette = await _context.Cassette
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cassette == null)
            {
                return NotFound();
            }

            return View(cassette);
        }

        // POST: Cassettes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cassette = await _context.Cassette.FindAsync(id);
            _context.Cassette.Remove(cassette);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CassetteExists(Guid id)
        {
            return _context.Cassette.Any(e => e.ID == id);
        }
    }
}
