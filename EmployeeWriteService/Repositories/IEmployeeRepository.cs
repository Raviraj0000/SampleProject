using EmployeeWriteService.Models;

namespace EmployeeWriteService.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetAsync(Guid employeeId);
        Task SaveAsync(Employee employee);
    }
}
