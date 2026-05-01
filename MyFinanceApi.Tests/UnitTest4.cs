using MySecureApi.Application.DTOs;
using MySecureApi.Application.Services;
using Domain;
using Domain.Interfaces;
using MySecureApi.Infrastructure;
using Moq;
using Xunit;


namespace MyFinanceApi.Tests;

public class UnitTest4
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly Mock<ITransactionAIService> _mockAIService;
    private readonly TransactionService _service;

    public UnitTest4() { 
        _mockRepo = new Mock<ITransactionRepository>();
        _mockAIService = new Mock<ITransactionAIService>();
        _service = new TransactionService(_mockRepo.Object , _mockAIService.Object);
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
