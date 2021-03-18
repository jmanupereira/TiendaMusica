using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaMusica.Data;
using TiendaMusica.Models;
using Microsoft.AspNetCore.Authorization;

namespace TiendaMusica.Controllers
{
    public class VinilosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VinilosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vinilos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vinilo.ToListAsync());
        }

        // GET: Vinilos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinilo = await _context.Vinilo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vinilo == null)
            {
                return NotFound();
            }

            return View(vinilo);
        }

        // GET: Vinilos/Create
        [Authorize(Roles="Admin, Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vinilos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Sello,CodigoSello,Artista,Titulo,Pais,Tipo,Pulgadas,RPM,FechaPublicacion,Genero,Estilo,Descripcion,Precio,Stock")] Vinilo vinilo)
        {
            if (ModelState.IsValid)
            {
                vinilo.ID = Guid.NewGuid();
                _context.Add(vinilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vinilo);
        }

        // GET: Vinilos/Edit/5
        [Authorize(Roles="Admin, Empleado")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinilo = await _context.Vinilo.FindAsync(id);
            if (vinilo == null)
            {
                return NotFound();
            }
            return View(vinilo);
        }

        // POST: Vinilos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Sello,CodigoSello,Artista,Titulo,Pais,Tipo,Pulgadas,RPM,FechaPublicacion,Genero,Estilo,Descripcion,Precio,Stock")] Vinilo vinilo)
        {
            if (id != vinilo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vinilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViniloExists(vinilo.ID))
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
            return View(vinilo);
        }

        // GET: Vinilos/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinilo = await _context.Vinilo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vinilo == null)
            {
                return NotFound();
            }

            return View(vinilo);
        }

        // POST: Vinilos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vinilo = await _context.Vinilo.FindAsync(id);
            _context.Vinilo.Remove(vinilo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViniloExists(Guid id)
        {
            return _context.Vinilo.Any(e => e.ID == id);
        }
    }
}
