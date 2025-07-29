using EmployeeWriteService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeWriteService.Persistence
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}
