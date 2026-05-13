using MediatR;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Interfaces;
using MySecureApi.Application.Queries;
using System.Linq;

namespace MySecureApi.Application.Handlers;

public class GetMyTransactionsQueryHandler
    : IRequestHandler<GetMyTransactionsQuery, ApiResponse<List<TransactionResponseDto>>>
{
    private readonly ITransactionRepository _repo;

    public GetMyTransactionsQueryHandler(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<List<TransactionResponseDto>>> Handle(
        GetMyTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        var transactions = await _repo.GetByUserIdAsync(query.UserId);

        var dtos = transactions.Select(t => new TransactionResponseDto
        {
            Id = t.Id,
            Amount = t.Amount,
            Category = t.Category,
            Description = t.Description,
            Date = t.Date,
            UserName = t.User?.Name ?? "Unknown"
        }).ToList();

        return ApiResponse<List<TransactionResponseDto>>.SuccessResponse(dtos);
    }
}