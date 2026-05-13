using MediatR;
using MySecureApi.Application.DTOs;

namespace MySecureApi.Application.Commands;

public record DeleteTransactionCommand : IRequest<ApiResponse<bool>>
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }

    public DeleteTransactionCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}