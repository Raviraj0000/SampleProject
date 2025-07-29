namespace EmployeeWriteService.Domain
{
    public class Events
    {
        public record EmployeeStatusUpdated(Guid EmployeeId, decimal Salary, DateTime UpdatedAt);
    }
}
