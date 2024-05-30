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
    internal class GetBugsTest
    {
        private IApplicationDbContext db;
        private Bug bugOpen;
        private Bug bugClosed;

        [SetUp]
        public async Task Setup()
        {
            db = Testing.GetTestContext();
            
            bugOpen = new Bug();
            bugOpen.DateClosed = null;

            bugClosed = new Bug();
            bugClosed.DateClosed = DateTime.UtcNow;

            db.Bugs.Add(bugOpen);
            db.Bugs.Add(bugClosed);
            var dbSave = db.SaveChangesAsync(CancellationToken.None);

            await dbSave;
        }

        [Test]
        public async Task GetBugs_QueryHandler_Handle_OnlyOpen_Test()
        {
            var query = new GetBugsQuery() {
                OnlyOpen = true,
                OnlyClosed = false,
            };

            var handler = new GetBugsQueryHandler(db, Testing._mapper);
            //--

            var bugs = await handler.Handle(query, CancellationToken.None);
            //--

            Assert.True(bugs.Count() >= 1);
            Assert.True(bugs.Any(x => x.BugId == bugOpen.BugId));
            Assert.True(!bugs.Any(x => x.BugId == bugClosed.BugId));
        }

        [Test]
        public async Task GetBugs_QueryHandler_Handle_OnlyClosed_Test()
        {
            var query = new GetBugsQuery()
            {
                OnlyOpen = false,
                OnlyClosed = true,
            };

            var handler = new GetBugsQueryHandler(db, Testing._mapper);
            //--

            var bugs = await handler.Handle(query, CancellationToken.None);
            //--

            Assert.True(bugs.Count() >= 1);
            Assert.True(!bugs.Any(x => x.BugId == bugOpen.BugId));
            Assert.True(bugs.Any(x => x.BugId == bugClosed.BugId));
        }
    }
}
