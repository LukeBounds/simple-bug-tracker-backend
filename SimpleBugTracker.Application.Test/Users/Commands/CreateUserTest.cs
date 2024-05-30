using SimpleBugTracker.Application.Users.Commands;
using SimpleBugTracker.Application.Users.Dto;

namespace SimpleBugTracker.Application.Test.Users.Commands
{
    internal class CreateUserTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task CreateUserTest1()
        {
            var db = Testing.GetTestContext();

            var userDto = new UserDto();

            var command = new CreateUserCommand()
            {
                User = userDto,
            };

            var handler = new CreateUserCommandHandler(db, Testing._mapper);
            //--

            var userId = await handler.Handle(command, CancellationToken.None);

            var addedUser = db.Users.Find(userId);
            //--

            Assert.True(userId != 0);
            Assert.IsNotNull(addedUser);
        }
    }
}
