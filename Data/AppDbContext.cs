using Microsoft.EntityFrameworkCore;
using Ventixe.RegistrationService.Models;

namespace Ventixe.RegistrationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Registration> Registrations { get; set; }
    }
}
