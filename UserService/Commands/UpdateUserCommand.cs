using MediatR;

namespace UserService.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
