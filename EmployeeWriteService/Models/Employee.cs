using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmployeeWriteService.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string ProfilePath { get; set; }
        public DateTime LastStatusUpdatedAt { get; set; }

        // Constructor (simplified)
        public Employee(Guid id, string firstName, string lastName, string department, decimal salary, string profilePath)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            Salary = salary;
            ProfilePath = profilePath;
            LastStatusUpdatedAt = DateTime.UtcNow;
        }

        // 🛠 Status transition logic
        public void UpdateStatus(decimal salary)
        {                     
            if (Salary == salary)
                return; // no-op if already set

            Salary = salary;
            LastStatusUpdatedAt = DateTime.UtcNow;
        }
    }
}
