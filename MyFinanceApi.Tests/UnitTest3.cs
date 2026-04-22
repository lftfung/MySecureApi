using Moq;
using Xunit;
using Application.Services;
using Application.DTOs;
using Domain.Interfaces;
using Domain;


namespace MyFinanceApi.Tests;

public class UnitTest3
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly TransactionService _service;

    public UnitTest3() { 
        _mockRepo = new Mock<ITransactionRepository>();
        _service = new TransactionService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetById_ShouldReturnResponseDto_WhenTransactionExists() {

        var transactionId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var existingTransaction = new AppTransaction { 
            Id = transactionId, 
            UserId = userId,
            Amount = 500,
            Category = "Transport",
            user = new User { Name = "Jack"}
        
        };

        _mockRepo.Setup(r => r.GetByIdAsync(transactionId, userId))
                 .ReturnsAsync(existingTransaction);

        var result = await _service.GetById(transactionId, userId);

        Assert.NotNull(result);
        Assert.Equal(500, result.Amount);
        Assert.Equal("Transport", result.Category);
        Assert.Equal("Jack", result.UserName);

    }
}
