using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountriesProyectTests.Services
{
    public class CitiesServiceTest
    {
        [Fact]
        public void Add_City_To_Database()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_state_to_database")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                service.addState(stateToSave);
            }
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.states.ToList());
                Assert.Equal("Buenos Aires", context.states.FirstOrDefault().Name);
            }
        }
    }
}
