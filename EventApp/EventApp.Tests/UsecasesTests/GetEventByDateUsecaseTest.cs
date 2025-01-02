using Moq;
using MediatR;
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
    public class GetEventByDateHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetEventByDateHandler _handler;

        public GetEventByDateHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetEventByDateHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEventResponse_WhenEventIsFound()
        {
            // Arrange
            var eventDate = new DateTime(2024, 12, 29);
            var eventEntity = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Event 1",
                Description = "Description 1",
                DateTime = eventDate,
                Location = new Location { Address = "123 Street", City = "City", State = "State", Country = "Country" },
                Category = "Music",
                MaxUsers = 100,
                ImageId = Guid.NewGuid()
            };

            var eventResponse = new EventResponse(
                eventEntity.Id, eventEntity.Title, eventEntity.Description, eventEntity.DateTime,
                new LocationResponse(eventEntity.Location.Address, eventEntity.Location.City, eventEntity.Location.State, eventEntity.Location.Country),
                eventEntity.Category, eventEntity.MaxUsers, "ImageData", "ImageType", eventEntity.ImageId);

           
            _unitOfWorkMock.Setup(u => u.Events.GetEventByDateAsync(eventDate)).ReturnsAsync(eventEntity);

           
            _mapperMock.Setup(m => m.Map<EventResponse>(eventEntity)).Returns(eventResponse);

            // Act
            var result = await _handler.Handle(new GetEventByDateRequest(eventDate), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventEntity.Title, result.Title);
            Assert.Equal(eventEntity.Description, result.Description);
            Assert.Equal(eventEntity.DateTime, result.DateTime);
        }
    }
}
