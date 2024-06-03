using SimpleBugTracker.Application.Bugs.Commands;
using SimpleBugTracker.Domain.Entities;
using SimpleBugTracker.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBugTracker.Application.Users.Commands;
using SimpleBugTracker.Application.Users.Dto;
using SimpleBugTracker.Application.Bugs.Dto;
using SimpleBugTracker.Application.Users.Services;

namespace SimpleBugTracker.Application.Test.Bugs.Commands
{
    internal class UpdateBugTest
    {
        IUserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
        }

        [Test]
        public async Task UpdateBug_CommandHandler_Handle_Test()
        {
            var db = Testing.GetTestContext();
            
            var userA = new User();
            var userB = new User();
            db.Users.Add(userA);
            db.Users.Add(userB);
            await db.SaveChangesAsync(CancellationToken.None);

            var bug = new Bug();
            bug.AssignedUser = userA;
            db.Bugs.Add(bug);
            await db.SaveChangesAsync(CancellationToken.None);


            var userBDto = Testing._mapper.Map<UserDto>(userB);

            var bugDto = Testing._mapper.Map<BugDto>(bug);
            bugDto.Title = "Test";
            bugDto.Description = "Test";
            bugDto.Priority = PriorityLevel.High;
            bugDto.DateCreated = DateTime.UtcNow;
            bugDto.DateClosed = DateTime.UtcNow;
            bugDto.AssignedUser = userBDto;


            var command = new UpdateBugCommand()
            {
                Bug = bugDto,
            };

            var handler = new UpdateBugCommandHandler(db, _userService);
            //--

            var bugId = await handler.Handle(command, CancellationToken.None);

            var updatedBug = db.Bugs.Find(bugId);
            //--

            Assert.True(bugId != 0);
            Assert.IsNotNull(updatedBug);
            Assert.That(bug.BugId, Is.EqualTo(updatedBug.BugId));

            Assert.That(bugDto.Title, Is.EqualTo(updatedBug.Title));
            Assert.That(bugDto.Description, Is.EqualTo(updatedBug.Description));
            Assert.That(bugDto.Priority, Is.EqualTo(updatedBug.Priority));
            Assert.That(bugDto.DateCreated, Is.Not.EqualTo(updatedBug.DateCreated));
            Assert.That(bugDto.DateClosed, Is.Not.EqualTo(updatedBug.DateClosed));
            Assert.That(bugDto.AssignedUser.UserId, Is.EqualTo(updatedBug.AssignedUser.UserId));           
        }
    }
}
