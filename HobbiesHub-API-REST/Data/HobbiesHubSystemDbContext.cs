using HobbiesHub_API_REST.Models;
using Microsoft.EntityFrameworkCore;

namespace HobbiesHub_API_REST.Data
{
    public class HobbiesHubSystemDbContext : DbContext
    {
        public HobbiesHubSystemDbContext(DbContextOptions<HobbiesHubSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
