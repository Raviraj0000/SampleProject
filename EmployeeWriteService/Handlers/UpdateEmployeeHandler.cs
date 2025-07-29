using EmployeeWriteService.Commands;
using EmployeeWriteService.Interfaces;
using EmployeeWriteService.Persistence;
using MediatR;

namespace EmployeeWriteService.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly WriteDbContext _context;
        private readonly IAppLogger<AddEmployeeHandler> _logger;

        public UpdateEmployeeHandler(WriteDbContext context, IAppLogger<AddEmployeeHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }        

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(request.Id);
            if (employee == null)
            {
                _logger.LogWarning($"Employee with ID {request.Id} not found.");
                return false;
            }
            if (employee == null) return false;

            employee.Department = request.Department;
            employee.Salary = request.Salary;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
