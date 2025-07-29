using MediatR;
using UserService.Commands;
using UserService.Persistence;

namespace UserService.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserDbContext _context;
        public UpdateUserHandler(UserDbContext context) => _context = context;

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null) return false;

            user.Role = request.Role;
            user.Email = request.Email;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
