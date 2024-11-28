using Firebase_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Firebase_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ChatMessageModel> Mensagens { get; set; }
        public DbSet<GrupoModel> Grupos { get; set; }
    }
}
