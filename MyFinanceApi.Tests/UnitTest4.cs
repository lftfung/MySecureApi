using Moq;
using Xunit;
using Application.Services;
using Application.DTOs;
using Domain.Interfaces;
using Domain;


namespace MyFinanceApi.Tests;

public class UnitTest4
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly TransactionService _service;

    public UnitTest4() { 
        _mockRepo = new Mock<ITransactionRepository>();
        _service = new TransactionService(_mockRepo.Object);
    }

    [Fact]
    public async Task Update_ShouldReturnTrue_WhenTransactionExists() {

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

        var result = await _service.Update(transactionId, userId, updateDto);
        
        Assert.True(result);
        Assert.Equal(200, existingTransaction.Amount);

        _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}
