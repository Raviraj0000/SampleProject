using MediatR;

namespace EmployeeWriteService.Commands
{
    public class AddEmployeeCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public string ProfilePath { get; set; } // S3 Key
    }
}
