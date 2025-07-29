using MediatR;
using UserService.Models;

namespace UserService.Queries
{
    public class GetUserQuery : IRequest<User>
    {
        public Guid Id { get; set; }
    }
}
