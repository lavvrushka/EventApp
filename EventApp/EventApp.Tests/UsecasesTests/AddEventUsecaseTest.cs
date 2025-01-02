using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Location.Request;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Models;
using EventApp.Domain.ValueObjects;
using MediatR;
using Moq;

namespace EventApp.Tests.UsecasesTests
{
    public class AddEventHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AddEventHandler _handler;


        public AddEventHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new AddEventHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldMapAndAddEvent()
        {
            var addEventRequest = new AddEventRequest(
                "Sample Event",
                "Sample Description",
                DateTime.Now,
                new LocationRequest
                (
                    "123 Event St.",
                    "Event City",
                    "Event State",
                    "Event Country"
                ),
                "Music",
                "imageData",
                "imageType",
                100
            );

            var location = new Location
            {
                Address = "123 Event St.",
                City = "Event City",
                State = "Event State",
                Country = "Event Country"
            };
            var eventEntity = new Event {};
            var image = new Image {};
            var imageId = Guid.NewGuid(); 

            _mapperMock.Setup(m => m.Map<Location>(It.IsAny<LocationRequest>())).Returns(location);
            _mapperMock.Setup(m => m.Map<Event>(It.IsAny<AddEventRequest>())).Returns(eventEntity);
            _mapperMock.Setup(m => m.Map<Image>(It.IsAny<AddEventRequest>())).Returns(image);
            _unitOfWorkMock.Setup(u => u.Images.AddImageToEventAsync(It.IsAny<Image>()))
                .ReturnsAsync(imageId);  
            _unitOfWorkMock.Setup(u => u.Events.AddAsync(It.IsAny<Event>())).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(addEventRequest, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _mapperMock.Verify(m => m.Map<Location>(It.IsAny<LocationRequest>()), Times.Once);
            _mapperMock.Verify(m => m.Map<Event>(It.IsAny<AddEventRequest>()), Times.Once);
            _mapperMock.Verify(m => m.Map<Image>(It.IsAny<AddEventRequest>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Images.AddImageToEventAsync(It.IsAny<Image>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.Events.AddAsync(It.IsAny<Event>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        
    }
}
