using SimpleBugTracker.Application.Bugs.Queries;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Test.Bugs.Queries
{
    internal class GetBugTest
    {
        private IApplicationDbContext db;

        [SetUp]
        public async Task Setup()
        {
        }

        [Test]
        public async Task GetBug_QueryHandler_Handle_Test()
        {
            db = Testing.GetTestContext();

            var bug = new Bug();

            db.Bugs.Add(bug);
            var dbSave = db.SaveChangesAsync(CancellationToken.None);
            await dbSave;

            var query = new GetBugQuery() {
                BugId = bug.BugId,
            };

            var handler = new GetBugQueryHandler(db, Testing._mapper);
            //--

            var bugRetrevied = await handler.Handle(query, CancellationToken.None);
            //--

            Assert.That(bug.BugId, Is.EqualTo(bugRetrevied.BugId));
        }
    }
}
