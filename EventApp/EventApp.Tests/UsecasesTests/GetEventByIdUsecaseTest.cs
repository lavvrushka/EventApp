using Moq;
using MediatR;
using AutoMapper;
using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Domain.Models;
using EventApp.Application.Common.Exeptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using EventApp.Application.DTOs.Location.Response;
using EventApp.Domain.ValueObjects;

namespace EventApp.Tests.UsecasesTests
{
    public class GetEventByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetEventByIdHandler _handler;

        public GetEventByIdHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetEventByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnEventResponse_WhenEventIsFound()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var eventEntity = new Event
            {
                Id = eventId,
                Title = "Event 1",
                Description = "Description 1",
                DateTime = DateTime.Now,
                Location = new Location { Address = "123 Street", City = "City", State = "State", Country = "Country" },
                Category = "Music",
                MaxUsers = 100,
                ImageId = Guid.NewGuid()
            };

            var eventResponse = new EventResponse(
                eventEntity.Id, eventEntity.Title, eventEntity.Description, eventEntity.DateTime,
                new LocationResponse (eventEntity.Location.Address, eventEntity.Location.City, eventEntity.Location.State, eventEntity.Location.Country),
                eventEntity.Category, eventEntity.MaxUsers, "ImageData", "ImageType", eventEntity.ImageId);

            
            _unitOfWorkMock.Setup(u => u.Events.GetByIdAsync(eventId)).ReturnsAsync(eventEntity);

           
            _mapperMock.Setup(m => m.Map<EventResponse>(eventEntity)).Returns(eventResponse);

            // Act
            var result = await _handler.Handle(new GetEventByIdRequest(eventId), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventEntity.Title, result.Title);
            Assert.Equal(eventEntity.Description, result.Description);
            Assert.Equal(eventEntity.DateTime, result.DateTime);
        }

       
    }
}
