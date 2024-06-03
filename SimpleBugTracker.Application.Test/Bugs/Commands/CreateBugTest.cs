using SimpleBugTracker.Application.Bugs.Commands;
using SimpleBugTracker.Domain.Entities;
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
    internal class CreateBugTest
    {
        IUserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService();
        }

        [Test]
        public async Task CreateBug_CommandHandler_Handle_Test()
        {
            var db = Testing.GetTestContext();
            
            var user = new User();
            db.Users.Add(user);
            await db.SaveChangesAsync(CancellationToken.None);

            var userDto = Testing._mapper.Map<UserDto>(user);

            var bugDto = new BugDto();
            bugDto.AssignedUser = userDto;

            var command = new CreateBugCommand()
            {
                Bug = bugDto,
            };

            var handler = new CreateBugCommandHandler(db, Testing._mapper, _userService);
            //--

            var bugId = await handler.Handle(command, CancellationToken.None);

            var addedBug = db.Bugs.Find(bugId);
            //--

            Assert.True(bugId != 0);
            Assert.IsNotNull(addedBug);
            Assert.That(user.UserId, Is.EqualTo(addedBug.AssignedUser.UserId));
        }
    }
}
