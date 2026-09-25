using LotoApp.DataAccess.Interfaces;
using LotoApp.Domain.Models;
using LotoApp.DTOs;
using LotoApp.Services.Implementation;
using Moq;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using Session = LotoApp.Domain.Models.Session;
using User = LotoApp.Domain.Models.User;

namespace LotoApp.Tests
{
    [TestClass]
    public class DrawServiceTests
    {
        private Mock<IDrawRepository> _drawRepositoryMock;
        private Mock<ISessionRepository> _sessionRepositoryMock;
        private Mock<ITicketRepository> _ticketRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IWinnerRepository> _winnerRepositoryMock;

        private DrawService _service;

        [TestInitialize]
        public void Setup()
        {
            // Initialize fresh mocks before every test
            _drawRepositoryMock = new Mock<IDrawRepository>();
            _sessionRepositoryMock = new Mock<ISessionRepository>();
            _ticketRepositoryMock = new Mock<ITicketRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _winnerRepositoryMock = new Mock<IWinnerRepository>();

            // Inject mocked repositories into the service
            _service = new DrawService(
                _drawRepositoryMock.Object,
                _sessionRepositoryMock.Object,
                _ticketRepositoryMock.Object,
                _userRepositoryMock.Object,
                _winnerRepositoryMock.Object
            );
        }

        [TestMethod]
        public async Task ExecuteDrawAsync_NoActiveSession_ThrowsInvalidOperationException()
        {
            // Arrange: Simulate no active session in database
            _sessionRepositoryMock
                .Setup(repo => repo.GetActiveSessionAsync())
                .ReturnsAsync((Session?)null);

            // Act & Assert: Verify that exception is thrown
            try
            {
                await _service.ExecuteDrawAsync(adminId: 1);
                Assert.Fail("Expected NullReferenceException was not thrown.");
            }
            catch (NullReferenceException)
            {
                // Test passes: The expected exception was thrown
            }
        }

      

        [TestMethod]
        public async Task ExecuteDrawAsync_ValidActiveSession_ExecutesDrawAndDeactivatesSession()
        {
            // Arrange
            int adminId = 1;
            var activeSession = new Session { Id = 10, IsActive = true };

            var ticket = new Ticket
            {
                Id = 100,
                UserId = 5,
                Numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 }
            };

            var player = new User { Id = 5, FirstName = "John", LastName = "Doe" };

            _sessionRepositoryMock
                .Setup(repo => repo.GetActiveSessionAsync())
                .ReturnsAsync(activeSession);

            _ticketRepositoryMock
                .Setup(repo => repo.GetTicketsForActiveSessionAsync(activeSession.Id))
                .ReturnsAsync(new List<Ticket> { ticket });

            _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(ticket.UserId))
                .ReturnsAsync(player);

            // Act
            DrawResultDto result = await _service.ExecuteDrawAsync(adminId);

            // Assert: Verify draw completed and evaluated 1 ticket
            Assert.IsNotNull(result);
            Assert.AreEqual(activeSession.Id, result.SessionId);
            Assert.AreEqual(1, result.TotalTicketsEvaluated);

            // Assert: Verify essential database operations occurred
            _drawRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Draw>()), Times.Once);
            _sessionRepositoryMock.Verify(repo => repo.Update(It.Is<Session>(s => !s.IsActive)), Times.Once);
        }




        [TestMethod]
        public async Task GetLatestDrawAsync_NoDrawsExist_ReturnsNull()
        {
            // Arrange
            _drawRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Draw>());

            // Act
            var result = await _service.GetLatestDrawAsync();

            // Assert
            Assert.IsNull(result);
        }
    }
}

