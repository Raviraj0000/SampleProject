using EmployeeWriteService.Commands;
using EmployeeWriteService.Interfaces;
using EmployeeWriteService.Models;
using EmployeeWriteService.Persistence;
using EmployeeWriteService.Services;
using MediatR;

namespace EmployeeWriteService.Handlers
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployeeCommand, Guid>
    {
        private readonly WriteDbContext _context;
        private readonly IS3Uploader _uploader;
        private readonly IAppLogger<AddEmployeeHandler> _logger;

        public AddEmployeeHandler(WriteDbContext context, IS3Uploader uploader, IAppLogger<AddEmployeeHandler> logger)
        {
            _context = context;
            _uploader = uploader;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var profileUrl = await _uploader.UploadAsync(request.ProfilePath);
            if (string.IsNullOrEmpty(profileUrl))
            {
                _logger.LogError("Failed to upload profile image.");
                throw new InvalidOperationException("Profile image upload failed.");
            }
            // Replace object initializer with constructor call to satisfy required parameters
            var employee = new Employee(
                Guid.NewGuid(),
                request.FirstName,
                request.LastName,
                request.Department,
                request.Salary,
                profileUrl
            )
            {
                LastStatusUpdatedAt = DateTime.UtcNow
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }
    }
}
