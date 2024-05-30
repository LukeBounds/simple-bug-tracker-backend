using AutoMapper;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Application.Bugs.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;

namespace SimpleBugTracker.Application.Bugs.Queries
{

    public record GetBugQuery : IRequest<BugDto>
    {
        public int BugId { get; set; }
    }

    public class GetBugQueryHandler : IRequestHandler<GetBugQuery, BugDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBugQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BugDto> Handle(GetBugQuery request, CancellationToken cancellationToken)
        {
            var bugId = request.BugId;
            var bug = await _context.Bugs.Include(x => x.AssignedUser).SingleAsync(x => x.BugId == bugId);
            Guard.Against.NotFound(bugId, bug);

            var bugDto = _mapper.Map<BugDto>(bug);

            return bugDto;
        }
    }
}
