using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using EmployeeReadService.Interfaces;
using EmployeeReadService.Models;
using EmployeeReadService.Persistent;
using EmployeeReadService.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EmployeeReadService.Handlers
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, EmployeeDTO>
    {
        private readonly ReadDbContext _context;
        private readonly IAppLogger<GetEmployeeHandler> _logger;

        public GetEmployeeHandler(ReadDbContext context, IAppLogger<GetEmployeeHandler> _logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
        }
        

        public async Task<EmployeeDTO> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var emp = await _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new EmployeeDTO
                {
                    Id = x.Id,
                    FullName = $"{x.FirstName} {x.LastName}",
                    Department = x.Department,
                    Salary = x.Salary,
                    ProfilePath = x.ProfilePath
                })
                .FirstOrDefaultAsync();

            return emp;
        }

        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach (var record in evnt.Records)
            {
                var payload = JsonSerializer.Deserialize<EmployeeStatusUpdated>(record.Body);
                if (payload is null) continue;

                var employee = await _context.Employees.FindAsync(payload.EmployeeId);
                if (employee == null)
                {
                    _logger.LogWarning($"Employee with ID {payload.EmployeeId} not found.");
                    continue;
                }

                if (employee != null)
                {
                    employee.Salary = payload.Salary;
                    employee.LastStatusUpdatedAt = payload.UpdatedAt;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
