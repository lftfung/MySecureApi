using MySecureApi.Application.DTOs;
using MySecureApi.Application.Services;
using Domain;
using MySecureApi.Application.Interfaces;
using Moq;
using Xunit;


namespace MyFinanceApi.Tests;

public class UnitTest1
{
    private readonly Mock<ITransactionRepository> _mockRepo;
    private readonly Mock<ITransactionAIService> _mockAIService;
    private readonly TransactionService _service;

    public UnitTest1() { 
        _mockRepo = new Mock<ITransactionRepository>();
        _mockAIService = new Mock<ITransactionAIService>();
        _service = new TransactionService(_mockRepo.Object, _mockAIService.Object);
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
