using MediatR;

namespace EmployeeWriteService.Commands
{
    public class UpdateEmployeeStatusCommand : IRequest
    {
        public Guid EmployeeId { get; init; }
        public decimal Salary { get; init; }

        public UpdateEmployeeStatusCommand(Guid employeeId, decimal salary)
        {
            EmployeeId = employeeId;
            Salary = salary;
        }
    }
}
