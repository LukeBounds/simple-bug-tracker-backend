using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Application.Users.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SimpleBugTracker.Application.Users.Queries
{
    public record GetUsersQuery : IRequest<ICollection<UserDto>>
    {

    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ICollection<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return users;
        }
    }
}
