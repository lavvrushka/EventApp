using Moq;
using MediatR;
using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using EventApp.Application.DTOs.Location.Response;
using EventApp.Domain.ValueObjects;

namespace EventApp.Tests.UsecasesTests
{
    public class GetAllEventsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllEventsHandler _handler;

        public GetAllEventsHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAllEventsHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEventResponses_WhenEventsExist()
        {
            // Arrange
            var eventEntities = new List<Event>
    {
        new Event
        {
            Id = Guid.NewGuid(),
            Title = "Event 1",
            Description = "Description 1",
            DateTime = DateTime.Now,
            Location = new Location { Address = "123 Street", City = "City", State = "State", Country = "Country" },
            Category = "Music",
            MaxUsers = 100,
            ImageId = Guid.NewGuid()
        },
        new Event
        {
            Id = Guid.NewGuid(),
            Title = "Event 2",
            Description = "Description 2",
            DateTime = DateTime.Now.AddMonths(1),
            Location = new Location { Address = "456 Avenue", City = "City2", State = "State2", Country = "Country2" },
            Category = "Sports",
            MaxUsers = 200,
            ImageId = Guid.NewGuid()
        }
    };

            var eventResponses = new List<EventResponse>
    {
        new EventResponse(
            eventEntities[0].Id, eventEntities[0].Title, eventEntities[0].Description, eventEntities[0].DateTime,
            new LocationResponse(eventEntities[0].Location.Address, eventEntities[0].Location.City, eventEntities[0].Location.State, eventEntities[0].Location.Country),
            eventEntities[0].Category, eventEntities[0].MaxUsers, "ImageData", "ImageType", eventEntities[0].ImageId),
        new EventResponse(
            eventEntities[1].Id, eventEntities[1].Title, eventEntities[1].Description, eventEntities[1].DateTime,
            new LocationResponse(eventEntities[1].Location.Address, eventEntities[1].Location.City, eventEntities[1].Location.State, eventEntities[1].Location.Country),
            eventEntities[1].Category, eventEntities[1].MaxUsers, "ImageData", "ImageType", eventEntities[1].ImageId)
    };

            _unitOfWorkMock.Setup(u => u.Events.GetAllAsync()).ReturnsAsync(eventEntities);  
            _mapperMock.Setup(m => m.Map<List<EventResponse>>(eventEntities)).Returns(eventResponses);  

            // Act
            var result = await _handler.Handle(new GetAllEventsRequest(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Event 1", result[0].Title);
            Assert.Equal("Event 2", result[1].Title);

            _unitOfWorkMock.Verify(u => u.Events.GetAllAsync(), Times.Once);  
            _mapperMock.Verify(m => m.Map<List<EventResponse>>(eventEntities), Times.Once);  
        }


        [Fact]
        public async Task Handle_ShouldThrowException_WhenNoEventsFound()
        {
            // Arrange
            _unitOfWorkMock.Setup(u => u.Events.GetAllAsync()).ReturnsAsync(new List<Event>()); 

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetAllEventsRequest(), CancellationToken.None));
            Assert.Equal("No events found.", exception.Message);

            _unitOfWorkMock.Verify(u => u.Events.GetAllAsync(), Times.Once);  
            _mapperMock.Verify(m => m.Map<List<EventResponse>>(It.IsAny<List<Event>>()), Times.Never);  
        }
    }
}
