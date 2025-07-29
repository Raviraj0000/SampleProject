using EmployeeWriteService.Commands;
using EmployeeWriteService.Repositories;
using MediatR;
using static EmployeeWriteService.Domain.Events;
using static EmployeeWriteService.Infrastructure.Messaging;

namespace EmployeeWriteService.Handlers
{
    public class UpdateEmployeeStatusCommandHandler(IEmployeeRepository repo, Infrastructure.Messaging.IEmployeeEventPublisher publisher) : IRequestHandler<UpdateEmployeeStatusCommand>
    {
        private readonly IEmployeeRepository _repo = repo;
        private readonly IEmployeeEventPublisher _publisher = publisher;

        public async Task<Unit> Handle(UpdateEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repo.GetAsync(request.EmployeeId);
            employee.UpdateStatus(request.Salary);  // domain method
            await _repo.SaveAsync(employee);

            await _publisher.PublishStatusUpdatedAsync(new EmployeeStatusUpdated(
                request.EmployeeId,
                request.Salary,
                DateTime.UtcNow
            ));

            return Unit.Value;
        }

        Task IRequestHandler<UpdateEmployeeStatusCommand>.Handle(UpdateEmployeeStatusCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
