using Microsoft.EntityFrameworkCore;
using NotasUnivoDev.Models;

namespace NotasUnivoDev.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FacultiesModel> Faculties { get; set; }
        public DbSet<CareersModel> Careers { get; set; }
    }
    
}
