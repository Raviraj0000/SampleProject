using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;
using static EmployeeWriteService.Domain.Events;
using static EmployeeWriteService.Infrastructure.Messaging;

namespace EmployeeWriteService.Domain
{
    public class SqsEmployeeEventPublisher : IEmployeeEventPublisher
    {
        private readonly IAmazonSQS _sqs;
        private readonly string _queueUrl;

        public SqsEmployeeEventPublisher(IAmazonSQS sqs, IConfiguration config)
        {
            _sqs = sqs;
            _queueUrl = config["EmployeeStatusQueueUrl"];
        }

        public async Task PublishStatusUpdatedAsync(EmployeeStatusUpdated @event)
        {
            var messageBody = JsonSerializer.Serialize(@event);
            var request = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = messageBody
            };
            await _sqs.SendMessageAsync(request);
        }
    }
}
