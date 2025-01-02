using Moq;
using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Domain.Models;
using EventApp.Application.DTOs.Location.Response;
using EventApp.Domain.ValueObjects;
using EventApp.Domain.Intarfaces.IRepositories;

namespace EventApp.Tests.UsecasesTests
{
    public class GetEventByTitleHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetEventByTitleHandler _handler;

        public GetEventByTitleHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetEventByTitleHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEventResponse_WhenEventIsFound()
        {
            // Arrange
            var eventTitle = "Music Concert";
            var eventEntity = new Event
            {
                Id = Guid.NewGuid(),
                Title = eventTitle,
                Description = "A great music concert.",
                DateTime = DateTime.Now,
                Location = new Location { Address = "123 Street", City = "City", State = "State", Country = "Country" },
                Category = "Music",
                MaxUsers = 500,
                ImageId = Guid.NewGuid()
            };

            var eventResponse = new EventResponse(
                eventEntity.Id, eventEntity.Title, eventEntity.Description, eventEntity.DateTime,
                new LocationResponse (eventEntity.Location.Address, eventEntity.Location.City, eventEntity.Location.State, eventEntity.Location.Country),
                eventEntity.Category, eventEntity.MaxUsers, "ImageData", "ImageType", eventEntity.ImageId);

            
            _unitOfWorkMock.Setup(u => u.Events.GetEventByTitleAsync(eventTitle)).ReturnsAsync(eventEntity);
            _mapperMock.Setup(m => m.Map<EventResponse>(eventEntity)).Returns(eventResponse);

            // Act
            var result = await _handler.Handle(new GetEventByTitleRequest(eventTitle), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventEntity.Title, result.Title);
            Assert.Equal(eventEntity.Description, result.Description);
            Assert.Equal(eventEntity.DateTime, result.DateTime);
        }
    }
}
