using MediatR;
using MySecureApi.Application.DTOs;

namespace MySecureApi.Application.Commands;

public record CreateTransactionCommand : IRequest<ApiResponse<TransactionResponseDto>>
{
    public CreateTransactionDto Dto { get; init; }
    public Guid UserId { get; init; }

    public CreateTransactionCommand(CreateTransactionDto dto, Guid userId)
    {
        Dto = dto;
        UserId = userId;
    }
}