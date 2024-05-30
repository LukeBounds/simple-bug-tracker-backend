using SimpleBugTracker.Application.Bugs.Commands;
using SimpleBugTracker.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBugTracker.Application.Test.Bugs.Commands
{
    internal class CloseBugTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task CloseBug_CommandHandler_Handle_Test()
        {
            var db = Testing.GetTestContext();
            var bug = new Bug() 
            { 
                DateClosed = null,
            };

            db.Bugs.Add(bug);
            var dbSave = db.SaveChangesAsync(CancellationToken.None);

            
            var command = new CloseBugCommand()
            {
                BugId = bug.BugId,
            };

            var handler = new CloseBugCommandHandler(db);

            await dbSave;
            //--
            
            await handler.Handle(command, CancellationToken.None);

            var closedBug = db.Bugs.FirstOrDefault(x => x.BugId == bug.BugId);
            //--

            Assert.IsNotNull(closedBug);
            Assert.IsNotNull(closedBug.DateClosed);
        }
    }
}
