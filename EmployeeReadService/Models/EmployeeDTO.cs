namespace EmployeeReadService.Models
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string ProfilePath { get; set; }

        public DateTime LastStatusUpdatedAt {get;set;}
    }
}
