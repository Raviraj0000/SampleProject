namespace EmployeeReadService.Models
{
    public class EmployeeStatusUpdated
    {
        public Guid EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
