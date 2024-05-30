using Ardalis.GuardClauses;
using SimpleBugTracker.Application.Data;
using MediatR;

namespace SimpleBugTracker.Application.Bugs.Commands
{
    public record CloseBugCommand : IRequest<int>
    {
        public int BugId { get; set; }
    }

    public class CloseBugCommandHandler : IRequestHandler<CloseBugCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CloseBugCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CloseBugCommand request, CancellationToken cancellationToken)
        {
            var bugId = request.BugId;
            var bug = await _context.Bugs.FindAsync(bugId, cancellationToken);

            Guard.Against.NotFound(bugId, bug);

            bug.DateClosed = DateTime.UtcNow;
            
            await _context.SaveChangesAsync(cancellationToken);

            return bug.BugId;
        }
    }
}
