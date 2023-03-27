using ExamenNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenNetCore.Data
{
    public class ZapatillasContext: DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
