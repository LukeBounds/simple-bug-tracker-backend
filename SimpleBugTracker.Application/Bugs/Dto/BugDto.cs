using AutoMapper;
using SimpleBugTracker.Application.Users.Dto;
using SimpleBugTracker.Domain.Entities;
using SimpleBugTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Bugs.Dto
{
    public class BugDto
    {
        public int BugId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public PriorityLevel Priority { get; set; } = PriorityLevel.Low;

        public DateTime DateCreated { get; set; }
        public DateTime? DateClosed { get; set; }


        public UserDto AssignedUser { get; set; } = new UserDto();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Bug, BugDto>().ReverseMap();
            }
        }
    }
}
