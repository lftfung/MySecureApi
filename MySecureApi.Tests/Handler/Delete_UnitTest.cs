using Domain;
using MediatR;
using Moq;
using MySecureApi.Application.Commands;
using MySecureApi.Application.Handlers;
using MySecureApi.Application.Interfaces;
using Xunit;

namespace MySecureApi.Tests.Handler;

public class Delete_UnitTest
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly DeleteTransactionCommandHandler _handler;

    public Delete_UnitTest()
    {
        _mockRepo = new Mock<ITransactionRepository>();
        _handler = new DeleteTransactionCommandHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Delete_ShouldReturnTrue_WhenTransactionExists()
    {
        var transactionId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var existingTransaction = new AppTransaction { Id = transactionId, UserId = userId };

        _mockRepo.Setup(r => r.GetByIdAsync(transactionId, userId))
                 .ReturnsAsync(existingTransaction);

        var command = new DeleteTransactionCommand(transactionId, userId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result.Success);
        Assert.True(result.Data);

        _mockRepo.Verify(r => r.DeleteAsync(existingTransaction), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}