using Ardalis.GuardClauses;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Application.Users.Dto;
using SimpleBugTracker.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Users.Services
{
    public interface IUserService
    {
        Task<User> GetUserFromDto(IApplicationDbContext context, CancellationToken cancellationToken, UserDto dto);
    }

    public class UserService : IUserService
    {
        async public Task<User> GetUserFromDto(IApplicationDbContext context, CancellationToken cancellationToken, UserDto dto)
        {
            var assignedUserId = dto.UserId;
            var assignedUser = await context.Users.FindAsync(assignedUserId, cancellationToken);
            Guard.Against.NotFound(assignedUserId, assignedUser);
            return assignedUser;
        }
    }
}
