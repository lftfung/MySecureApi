using MediatR;
using MySecureApi.Application.DTOs;

namespace MySecureApi.Application.Queries;

public record GetMyTransactionsQuery : IRequest<ApiResponse<List<TransactionResponseDto>>>
{
    public Guid UserId { get; init; }

    public GetMyTransactionsQuery(Guid userId)
    {
        UserId = userId;
    }
}