using Moq;
using Xunit;
using Application.Services;
using Application.DTOs;
using Domain.Interfaces;
using Domain;


namespace MyFinanceApi.Tests;

public class UnitTest1
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly TransactionService _service;

    public UnitTest1() { 
        _mockRepo = new Mock<ITransactionRepository>();
        _service = new TransactionService(_mockRepo.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnTransaction_WhenDataIsVaild() {
        var dto = new CreateTransactionDto
        {
            Amount = 500,
            Category = "Food",
            Date = DateTime.UtcNow

        };

        var userId = Guid.NewGuid();

        var result = await _service.Create(dto, userId);
        Assert.NotNull(result);
        Assert.Equal(500, result.Amount);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<AppTransaction>()), Times.Once);
    
    }
}
