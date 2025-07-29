using MediatR;

namespace EmployeeWriteService.Commands
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}
