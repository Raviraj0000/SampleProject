using EmployeeReadService.Models;
using MediatR;

namespace EmployeeReadService.Queries
{
    public class GetEmployeeQuery : IRequest<EmployeeDTO>
    {
        public Guid Id { get; set; }
    }
}
