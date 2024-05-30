using Ardalis.GuardClauses;
using AutoMapper;
using SimpleBugTracker.Application.Bugs.Dto;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Application.Users.Services;
using SimpleBugTracker.Domain.Entities;
using MediatR;

namespace SimpleBugTracker.Application.Bugs.Commands
{
    public record CreateBugCommand : IRequest<int>
    {
        public BugDto? Bug { get; set; }
    }

    public class CreateBugCommandHandler : IRequestHandler<CreateBugCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public CreateBugCommandHandler(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<int> Handle(CreateBugCommand request, CancellationToken cancellationToken)
        {
            var bug = _mapper.Map<Bug>(request.Bug);

            bug.AssignedUser = await _userService.GetUserFromDto(_context, cancellationToken, request.Bug.AssignedUser);

            _context.Bugs.Add(bug);

            await _context.SaveChangesAsync(cancellationToken);

            return bug.BugId;
        }
    }
}
