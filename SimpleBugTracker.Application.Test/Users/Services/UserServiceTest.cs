using SimpleBugTracker.Application.Users.Dto;
using SimpleBugTracker.Application.Users.Services;
using SimpleBugTracker.Domain.Entities;

namespace SimpleBugTracker.Application.Test.Users.Services
{
    internal class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetUserFromDto_Test()
        {
            var db = Testing.GetTestContext();
            IUserService userService = new UserService();

            var user = new User();
            user.Name = "TestName";
            user.Name = "TestEmail";
            db.Users.Add(user);
            await db.SaveChangesAsync(CancellationToken.None);

            var userDto = Testing._mapper.Map<UserDto>(user);
            //--

            var retrievedUser = await userService.GetUserFromDto(db, CancellationToken.None, userDto);
            //--

            Assert.IsNotNull(retrievedUser);
            Assert.That(user.UserId, Is.EqualTo(retrievedUser.UserId));
            Assert.That(user.Name, Is.EqualTo(retrievedUser.Name));
            Assert.That(user.Email, Is.EqualTo(retrievedUser.Email));
        }
    }
}
