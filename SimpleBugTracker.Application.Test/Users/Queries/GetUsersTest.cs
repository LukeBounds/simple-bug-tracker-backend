using SimpleBugTracker.Application.Users.Commands;
using SimpleBugTracker.Application.Users.Dto;
using SimpleBugTracker.Application.Users.Queries;
using SimpleBugTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Test.Users.Queries
{
    internal class GetUsersTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetUsersTest1()
        {
            var db = Testing.GetTestContext();
            
            var user = new User();
            db.Users.Add(user);
            db.SaveChangesAsync(CancellationToken.None);

            var command = new GetUsersQuery();

            var handler = new GetUsersQueryHandler(db, Testing._mapper);
            //--

            var users = await handler.Handle(command, CancellationToken.None);
            //--

            Assert.True(users.Count() >= 1);
            Assert.That(user.UserId, Is.EqualTo(users.OrderBy(x => x.UserId).Last().UserId));
        }
    }
}
