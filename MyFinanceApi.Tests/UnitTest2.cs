using Moq;
using Xunit;
using Application.Services;
using Application.DTOs;
using Domain.Interfaces;
using Domain;


namespace MyFinanceApi.Tests;

public class UnitTest2
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly TransactionService _service;

    public UnitTest2() { 
        _mockRepo = new Mock<ITransactionRepository>();
        _service = new TransactionService(_mockRepo.Object);
    }

    [Fact]
    public async Task Delete_ShouldReturnTrue_WhenTransactionExists() {

        var transactionId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var existingTransaction = new AppTransaction { Id = transactionId, UserId = userId };

        _mockRepo.Setup(r => r.GetByIdAsync(transactionId, userId))
                 .ReturnsAsync(existingTransaction);

        var result = await _service.Delete(transactionId, userId);

        Assert.True(result); 

        _mockRepo.Verify(r => r.DeleteAsync(existingTransaction), Times.Once);
        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);

    }
}
