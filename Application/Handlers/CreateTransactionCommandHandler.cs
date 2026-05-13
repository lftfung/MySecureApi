using MediatR;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Interfaces;
using Domain;
using MySecureApi.Application.Commands;

namespace MySecureApi.Application.Handlers;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ApiResponse<TransactionResponseDto>>
{
    private readonly ITransactionRepository _repo;
    private readonly ITransactionAIService _aiService;

    public CreateTransactionCommandHandler(ITransactionRepository repo, ITransactionAIService aiService)
    {
        _repo = repo;
        _aiService = aiService;
    }

    public async Task<ApiResponse<TransactionResponseDto>> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        var dto = command.Dto;
        var userId = command.UserId;

        var category = dto.Category;
        if (string.IsNullOrWhiteSpace(category))
        {
            category = await _aiService.PredictCategoryAsync(dto.Description);
        }

        var transaction = new AppTransaction
        {
            Amount = dto.Amount,
            Category = category,
            Description = dto.Description,
            Date = dto.Date,
            UserId = userId
        };

        await _repo.AddAsync(transaction);
        await _repo.SaveChangesAsync();

        var responseDto = new TransactionResponseDto
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Category = transaction.Category,
            Description = transaction.Description,
            Date = transaction.Date,
            UserName = transaction.User?.Name
        };

        return ApiResponse<TransactionResponseDto>.SuccessResponse(responseDto, "Transaction created successfully");
    }
}