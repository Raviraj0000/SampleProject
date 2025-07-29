using EmployeeReadService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeReadService.Persistent
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }

        public DbSet<EmployeeDTO> Employees { get; set; }
    }
}
