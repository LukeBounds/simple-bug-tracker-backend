using AutoMapper;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Application.Users.Dto;
using SimpleBugTracker.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Users.Commands
{
    public record CreateUserCommand : IRequest<int>
    {
        public UserDto? User { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return user.UserId;
        }
    }
}
