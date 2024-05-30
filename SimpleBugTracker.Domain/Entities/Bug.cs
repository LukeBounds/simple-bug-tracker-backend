using SimpleBugTracker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Domain.Entities
{
    public class Bug
    {
        public int BugId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public PriorityLevel Priority { get; set; } = PriorityLevel.Low;

        public DateTime DateCreated { get; set; }
        public DateTime? DateClosed { get; set; }

        public virtual User AssignedUser { get; set; } = new User();
    }
}
