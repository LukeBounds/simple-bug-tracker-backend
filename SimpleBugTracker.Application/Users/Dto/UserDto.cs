using AutoMapper;
using SimpleBugTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Users.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<User, UserDto>().ReverseMap();
            }
        }
    }
}
