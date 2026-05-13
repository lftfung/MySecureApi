using Domain;
using MediatR;
using Moq;
using MySecureApi.Application.Commands;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Handlers;
using MySecureApi.Application.Interfaces;
using Xunit;

namespace MyFinanceApi.Tests;

public class Update_UnitTest
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly UpdateTransactionCommandHandler _handler;

    public Update_UnitTest()
    {
        _mockRepo = new Mock<ITransactionRepository>();
        _handler = new UpdateTransactionCommandHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Update_ShouldReturnTrue_WhenTransactionExists()
    {
        var transactionId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var existingTransaction = new AppTransaction
        {
            Id = transactionId,
            UserId = userId,
            Amount = 100
        };

        var updateDto = new UpdateTransactionDto
        {
            Amount = 200,
            Category = "Food",
            Date = DateTime.UtcNow
        };

        _mockRepo.Setup(r => r.GetByIdAsync(transactionId, userId))
                 .ReturnsAsync(existingTransaction);

        var command = new UpdateTransactionCommand(transactionId, updateDto, userId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.Success);
        Assert.True(result.Data);

        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}