using AutoMapper;
using SimpleBugTracker.Application.Data;
using SimpleBugTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SimpleBugTracker.Application.Test
{
    [SetUpFixture]
    internal class Testing
    {

        public static IMapper _mapper;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var configuration = new MapperConfiguration(config =>
                config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));
            _mapper = configuration.CreateMapper();
        }

        public static IApplicationDbContext GetTestContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

            return new ApplicationDbContext(options);
        }
    }
}
