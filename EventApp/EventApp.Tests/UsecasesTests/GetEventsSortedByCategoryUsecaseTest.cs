using Moq;
using AutoMapper;
using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Domain.Models;

namespace EventApp.Tests.UsecasesTests
{
    public class GetEventsSortedByCategoryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetEventsSortedByCategoryHandler _handler;

        public GetEventsSortedByCategoryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetEventsSortedByCategoryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSortedEventResponses_WhenEventsExist()
        {
            // Arrange
            var events = new List<Event>
    {
        
 
        new Event { Id = Guid.NewGuid(), Title = "Food Festival", Category = "Food", DateTime = DateTime.Now.AddMonths(2) },
        new Event { Id = Guid.NewGuid(), Title = "Music Concert", Category = "Music", DateTime = DateTime.Now },
               new Event { Id = Guid.NewGuid(), Title = "Tech Conference", Category = "Technology", DateTime = DateTime.Now.AddMonths(1) }
    };

            var eventResponses = new List<EventResponse>
    {
                    new EventResponse(Guid.NewGuid(), "Food Festival", "Description", DateTime.Now.AddMonths(2), null, "Food", 300, "imageData", "imageType", Guid.NewGuid()),
        new EventResponse(Guid.NewGuid(), "Music Concert", "Description", DateTime.Now, null, "Music", 100, "imageData", "imageType", Guid.NewGuid()),
        new EventResponse(Guid.NewGuid(), "Tech Conference", "Description", DateTime.Now.AddMonths(1), null, "Technology", 200, "imageData", "imageType", Guid.NewGuid())
    
    };

            _unitOfWorkMock.Setup(u => u.Events.GetEventsSortedByCategoryAsync()).ReturnsAsync(events);

            _mapperMock.Setup(m => m.Map<List<EventResponse>>(events)).Returns(eventResponses);

            // Act
            var result = await _handler.Handle(new GetEventsSortedByCategoryRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(events.Count, result.Count);
            Assert.Equal("Food", result[0].Category); 
            Assert.Equal("Music", result[1].Category);
            Assert.Equal("Technology", result[2].Category);
        }



    }
}
