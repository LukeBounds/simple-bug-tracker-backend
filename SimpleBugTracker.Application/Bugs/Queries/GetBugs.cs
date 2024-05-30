using AutoMapper;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Application.Bugs.Dto;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SimpleBugTracker.Domain.Entities;

namespace SimpleBugTracker.Application.Bugs.Queries
{

    public record GetBugsQuery : IRequest<ICollection<BugDto>>
    {
        public bool OnlyOpen { get; set; }
        public bool OnlyClosed { get; set; }
    }

    public class GetBugsQueryHandler : IRequestHandler<GetBugsQuery, ICollection<BugDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBugsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<BugDto>> Handle(GetBugsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Bug> bugs = _context.Bugs.AsQueryable();

            if (request.OnlyOpen)
            {
                bugs = bugs.Where(x => x.DateClosed == null);
            }
            else if (request.OnlyClosed)
            {
                bugs = bugs.Where(x => x.DateClosed.HasValue);
            }

            var bugList = await bugs.ProjectTo<BugDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return bugList;
        }
    }
}
