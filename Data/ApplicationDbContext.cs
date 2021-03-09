using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TiendaMusica.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TiendaMusica.Models.Vinilo> Vinilo { get; set; }

        public DbSet<TiendaMusica.Models.DiscoCompacto> DiscoCompacto { get; set; }

        public DbSet<TiendaMusica.Models.Cassette> Cassette { get; set; }
        
        public DbSet<TiendaMusica.Models.Otro> Otro { get; set; }
    }
}
