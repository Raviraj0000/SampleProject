using MediatR;
using UserService.Models;
using UserService.Persistence;
using UserService.Queries;

namespace UserService.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly UserDbContext _context;
        public GetUserHandler(UserDbContext context) => _context = context;

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
            => await _context.Users.FindAsync(request.Id);
    }
}
