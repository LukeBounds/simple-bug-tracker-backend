using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using SimpleBugTracker.Application.Users.Dto;

namespace SimpleBugTracker.Application.Users.Commands
{
    public record UpdateUserCommand : IRequest<int>
    {
        public UserDto? User { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = request.User.UserId;
            var user = await _context.Users.FindAsync(userId, cancellationToken);

            Guard.Against.NotFound(userId, user);

            if (user.Name != request.User.Name) { user.Name = request.User.Name; }
            if (user.Email != request.User.Email) { user.Email = request.User.Email; }

            await _context.SaveChangesAsync(cancellationToken);

            return user.UserId;
        }
    }
}
