using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.UseCases.EventUsecases;
using EventApp.Domain.Models;
using MediatR;
using Moq;

namespace EventApp.Tests.UsecasesTests
{
    public class DeleteEventHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteEventHandler _handler;

        public DeleteEventHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteEventHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteEvent()
        {
            // Arrange
            var eventId = Guid.NewGuid();
            var deleteEventRequest = new DeleteEventRequest(eventId);

            var eventEntity = new Event { Id = eventId, Title = "Sample Event" };

            _unitOfWorkMock.Setup(u => u.Events.GetByIdAsync(eventId))
                .ReturnsAsync(eventEntity);  
            _unitOfWorkMock.Setup(u => u.Events.DeleteAsync(eventEntity))
                .Returns(Task.CompletedTask); 
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(0); 

            // Act
            var result = await _handler.Handle(deleteEventRequest, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);  
            _unitOfWorkMock.Verify(u => u.Events.GetByIdAsync(eventId), Times.Once);  
            _unitOfWorkMock.Verify(u => u.Events.DeleteAsync(eventEntity), Times.Once);  
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);  
        }
    }
}
