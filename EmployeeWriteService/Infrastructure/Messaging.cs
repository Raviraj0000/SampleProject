using static EmployeeWriteService.Domain.Events;

namespace EmployeeWriteService.Infrastructure
{
    public class Messaging
    {
        public interface IEmployeeEventPublisher
        {
            Task PublishStatusUpdatedAsync(EmployeeStatusUpdated @event);
        }
    }
}
