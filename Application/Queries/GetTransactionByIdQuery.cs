using MediatR;
using MySecureApi.Application.DTOs;

namespace MySecureApi.Application.Queries;

public record GetTransactionByIdQuery : IRequest<ApiResponse<TransactionResponseDto>>
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public GetTransactionByIdQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}