using Domain;
using MediatR;
using Moq;
using MySecureApi.Application.Commands;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Handlers;
using MySecureApi.Application.Interfaces;
using Xunit;

namespace MySecureApi.Tests.Handler;

public class Create_UnitTest
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly Mock<ITransactionAIService> _mockAIService;
    private readonly CreateTransactionCommandHandler _handler;

    public Create_UnitTest()
    {
        _mockRepo = new Mock<ITransactionRepository>();
        _mockAIService = new Mock<ITransactionAIService>();
        _handler = new CreateTransactionCommandHandler(_mockRepo.Object, _mockAIService.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnTransaction_WhenDataIsValid()
    {
        // Arrange
        var dto = new CreateTransactionDto
        {
            Amount = 500,
            Category = "Food",
            Date = DateTime.UtcNow
        };
        var userId = Guid.NewGuid();

        _mockAIService.Setup(a => a.PredictCategoryAsync(It.IsAny<string>()))
                      .ReturnsAsync("Food");

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<AppTransaction>()))
                 .ReturnsAsync(new AppTransaction());

        var command = new CreateTransactionCommand(dto, userId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(500, result.Data.Amount);

        _mockRepo.Verify(r => r.AddAsync(It.IsAny<AppTransaction>()), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}