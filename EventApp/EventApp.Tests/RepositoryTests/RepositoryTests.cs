using EventApp.Domain.Models;
using EventApp.Domain.ValueObjects;
using EventApp.Infrastructure.Persistence.Context;
using EventApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Tests.RepositoryTests
{
    public class EventRepositoryTest
    {
        private ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEventToDatabase()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repository = new EventRepository(context);

            var eventEntity = new Event
            {
                Title = "Test Event",
                Description = "This is a test event.",
                DateTime = new DateTime(2024, 12, 31),
                Location = new Location
                {
                    Address = "123 Main St",
                    City = "Test City",
                    State = "Test State",
                    Country = "Test Country"
                },
                Category = "Conference",
                MaxUsers = 100,
                ImageId = Guid.NewGuid(),
            };

            // Act
            await repository.AddAsync(eventEntity);
            await context.SaveChangesAsync();

            // Assert
            var savedEvent = await context.Events
                .Include(e => e.Location)
                .FirstOrDefaultAsync(e => e.Title == "Test Event");

            Assert.NotNull(savedEvent);
            Assert.Equal("Test Event", savedEvent.Title);
            Assert.Equal("This is a test event.", savedEvent.Description);
        }



        [Fact]
        public async Task GetEventCountAsync_ShouldReturnCorrectEventCount()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            var context = new ApplicationDbContext(options);
            var repository = new EventRepository(context);

            
            var event1 = new Event
            {
                Title = "Event 1",
                Description = "Description 1",
                Category = "Conference",
                DateTime = new DateTime(2024, 12, 31),
                Location = new Location { Address = "Address 1", City = "City 1", State = "State 1", Country = "Country 1" },
                ImageId = Guid.NewGuid(),
                MaxUsers = 100
            };

            var event2 = new Event
            {
                Title = "Event 2",
                Description = "Description 2",
                Category = "Workshop",
                DateTime = new DateTime(2024, 12, 31),
                Location = new Location { Address = "Address 2", City = "City 2", State = "State 2", Country = "Country 2" },
                ImageId = Guid.NewGuid(),
                MaxUsers = 50
            };

            context.Events.AddRange(event1, event2);
            await context.SaveChangesAsync();  

            // Act
            var eventCount = await repository.GetEventCountAsync();

            // Assert
            Assert.Equal(2, eventCount);  
        }
    }
}
