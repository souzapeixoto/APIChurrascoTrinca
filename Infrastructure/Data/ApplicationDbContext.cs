
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opcoes)
            : base(opcoes)
        {

        }

        public DbSet<Churrasco> Churrascos { get; set; }
        public DbSet<Opcao> Opcoes { get; set; }
        public DbSet<Convidado> Convidados { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
