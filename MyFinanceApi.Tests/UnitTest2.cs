using MySecureApi.Application.DTOs;
using MySecureApi.Application.Services;
using Domain;
using Domain.Interfaces;
using MySecureApi.Infrastructure;
using Moq;
using Xunit;


namespace MyFinanceApi.Tests;

public class UnitTest2
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly Mock<ITransactionAIService> _mockAIService;
    private readonly TransactionService _service;

    public UnitTest2() { 
        _mockRepo = new Mock<ITransactionRepository>();
        _mockAIService = new Mock<ITransactionAIService>();
        _service = new TransactionService(_mockRepo.Object, _mockAIService.Object);
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
