using AutoMapper;
using EventApp.Application.Common.Exeptions;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Domain.Models;
using EventApp.Domain.ValueObjects;
using Moq;
using MediatR;
using EventApp.Application.DTOs.Location.Request;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Interfaces.IServices;

namespace EventApp.Tests.UsecasesTests
{
    public class UpdateEventHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IEmailNotificationService> _emailNotificationServiceMock;
        private readonly UpdateEventHandler _handler;

        public UpdateEventHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _emailNotificationServiceMock = new Mock<IEmailNotificationService>();
            _handler = new UpdateEventHandler(_unitOfWorkMock.Object, _mapperMock.Object, _emailNotificationServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenEventNotFound()
        {
            // Arrange
            var updateRequest = new UpdateEventRequest(
                Guid.NewGuid(),
                "Updated Event",
                "Updated Description",
                DateTime.Now,
                new LocationRequest("Updated Address", "Updated City", "Updated State", "Updated Country"),
                "Updated Category",
                Guid.NewGuid(),
                "Updated ImageData",
                "imageType",
                100
            );

            _unitOfWorkMock.Setup(u => u.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Event)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(updateRequest, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenImageNotFound()
        {
            // Arrange
            var updateRequest = new UpdateEventRequest(
                Guid.NewGuid(),
                "Updated Event",
                "Updated Description",
                DateTime.Now,
                new LocationRequest("Updated Address", "Updated City", "Updated State", "Updated Country"),
                "Updated Category",
                Guid.NewGuid(),
                "Updated ImageData",
                "imageType",
                100
            );

            var eventEntity = new Event { Id = updateRequest.Id, Title = "Sample Event", DateTime = DateTime.Now };

            
            _unitOfWorkMock.Setup(u => u.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(eventEntity);
            _unitOfWorkMock.Setup(u => u.Images.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Image)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(updateRequest, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldUpdateEventAndImage_WhenRequestIsValid()
        {
            // Arrange
            var updateRequest = new UpdateEventRequest(
                Guid.NewGuid(),
                "Updated Event",
                "Updated Description",
                DateTime.Now,
                new LocationRequest("Updated Address", "Updated City", "Updated State", "Updated Country"),
                "Updated Category",
                Guid.NewGuid(),
                "Updated ImageData",
                "imageType",
                100
            );

            var eventEntity = new Event { Id = updateRequest.Id, Title = "Sample Event", DateTime = DateTime.Now };
            var imageEntity = new Image { Id = updateRequest.ImageId, ImageData = "Old Image Data", ImageType = "imageType" };
            var location = new Location
            {
                Address = updateRequest.Location.Address,
                City = updateRequest.Location.City,
                State = updateRequest.Location.State,
                Country = updateRequest.Location.Country
            };

            
            _unitOfWorkMock.Setup(u => u.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(eventEntity);
            _unitOfWorkMock.Setup(u => u.Images.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(imageEntity);
            _mapperMock.Setup(m => m.Map(It.IsAny<UpdateEventRequest>(), It.IsAny<Event>())).Verifiable();
            _mapperMock.Setup(m => m.Map(It.IsAny<UpdateEventRequest>(), It.IsAny<Image>())).Verifiable();

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(updateRequest, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);

            _mapperMock.Verify(m => m.Map(It.IsAny<UpdateEventRequest>(), It.IsAny<Event>()), Times.Once);
            _mapperMock.Verify(m => m.Map(It.IsAny<UpdateEventRequest>(), It.IsAny<Image>()), Times.Once);

            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            _emailNotificationServiceMock.Verify(s => s.SendEventUpdateNotificationAsync(It.IsAny<Event>()), Times.Once);
        }
    }
}
