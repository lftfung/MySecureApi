using MediatR;
using MySecureApi.Application.DTOs;

namespace MySecureApi.Application.Commands;

public record UpdateTransactionCommand : IRequest<ApiResponse<bool>>
{
    public Guid Id { get; init; }
    public UpdateTransactionDto Dto { get; init; }
    public Guid UserId { get; init; }

    public UpdateTransactionCommand(Guid id, UpdateTransactionDto dto, Guid userId)
    {
        Id = id;
        Dto = dto;
        UserId = userId;
    }
}