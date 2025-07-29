using MediatR;

namespace UserService.Commands
{
    public class AddUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
