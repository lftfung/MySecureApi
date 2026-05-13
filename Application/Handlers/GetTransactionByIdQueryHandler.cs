using MediatR;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Interfaces;
using MySecureApi.Application.Queries;

namespace MySecureApi.Application.Handlers;

public class GetTransactionByIdQueryHandler
    : IRequestHandler<GetTransactionByIdQuery, ApiResponse<TransactionResponseDto>>
{
    private readonly ITransactionRepository _repo;

    public GetTransactionByIdQueryHandler(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<TransactionResponseDto>> Handle(
        GetTransactionByIdQuery query,
        CancellationToken cancellationToken)
    {
        var t = await _repo.GetByIdAsync(query.Id, query.UserId);

        if (t == null)
        {
            return ApiResponse<TransactionResponseDto>.ErrorResponse("Transaction not found or no permission");
        }

        var result = new TransactionResponseDto
        {
            Id = t.Id,
            Amount = t.Amount,
            Category = t.Category,
            Description = t.Description,
            Date = t.Date,
            UserName = t.User?.Name ?? "Unknown"
        };

        return ApiResponse<TransactionResponseDto>.SuccessResponse(result);
    }
}