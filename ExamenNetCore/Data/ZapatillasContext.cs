using ExamenNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenNetCore.Data
{
    public class ZapatillasContext: DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Zapatilla> Zapatillas { get; set; }
        public DbSet<ImagenZapa> ImagenZapas { get; set; }
        public DbSet<VistaImagenZapatilla> VistaImagenZapatillas { get; set; }
    }
}
