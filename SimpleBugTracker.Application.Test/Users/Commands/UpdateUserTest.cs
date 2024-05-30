using SimpleBugTracker.Application.Users.Commands;
using SimpleBugTracker.Application.Users.Dto;
using SimpleBugTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Test.Users.Commands
{
    internal class UpdateUserTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task UpdateUserTest1()
        {
            var db = Testing.GetTestContext();

            var user = new User();
            db.Users.Add(user);
            db.SaveChangesAsync(CancellationToken.None);

            var updatedUserDto = Testing._mapper.Map<UserDto>(user);
            updatedUserDto.Name = "Test";
            updatedUserDto.Email = "test@email.com";

            var command = new UpdateUserCommand()
            {
                User = updatedUserDto,
            };

            var handler = new UpdateUserCommandHandler(db);
            //--

            var userId = await handler.Handle(command, CancellationToken.None);

            var updatedUser = db.Users.Find(user.UserId);
            //--

            Assert.True(userId != 0);
            Assert.AreEqual(updatedUserDto.Name, updatedUser.Name);
            Assert.That(updatedUserDto.Email, Is.EqualTo(updatedUser.Email));
        }
    }
}
