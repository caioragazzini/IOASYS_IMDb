using IOASYS_IMDb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IOASYS_IMDb.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Filme> Filmes { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Diretor> Diretors { get; set; }

        public DbSet<Ator> Ators { get; set; }

        public DbSet<AtorFilme> AtorFilmes { get; set; }

        public DbSet<Voto> Voto { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}