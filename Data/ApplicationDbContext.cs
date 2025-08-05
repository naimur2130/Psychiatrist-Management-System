using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Psychiatrist_Management_System.Models;

namespace Psychiatrist_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PsychiatristProfile> PsychiatristProfile  { get; set; }
    }
}
