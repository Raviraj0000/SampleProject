using MediatR;
using UserService.Commands;
using UserService.Models;
using UserService.Persistence;

namespace UserService.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly UserDbContext _context;
        public AddUserHandler(UserDbContext context) => _context = context;

        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                Email = request.Email
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
    }
}
