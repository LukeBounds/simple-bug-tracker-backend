using Ardalis.GuardClauses;
using AutoMapper;
using SimpleBugTracker.Application.Bugs.Dto;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Application.Users.Services;
using MediatR;

namespace SimpleBugTracker.Application.Bugs.Commands
{
    public record UpdateBugCommand : IRequest<int>
    {
        public BugDto? Bug { get; set; }
    }

    public class UpdateBugCommandHandler : IRequestHandler<UpdateBugCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public UpdateBugCommandHandler(IApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<int> Handle(UpdateBugCommand request, CancellationToken cancellationToken)
        {
            var bugId = request.Bug.BugId;
            var bug = await _context.Bugs.FindAsync(bugId, cancellationToken);

            Guard.Against.NotFound(bugId, bug);

            if (bug.Title != request.Bug.Title) { bug.Title = request.Bug.Title; }
            if (bug.Description != request.Bug.Description) { bug.Description = request.Bug.Description; }
            if (bug.Priority != request.Bug.Priority) { bug.Priority = request.Bug.Priority; }
            if (bug.AssignedUser.UserId != request.Bug.AssignedUser.UserId)
            {
                bug.AssignedUser = await _userService.GetUserFromDto(_context, cancellationToken, request.Bug.AssignedUser);
            }
            
            await _context.SaveChangesAsync(cancellationToken);

            return bug.BugId;
        }
    }
}
