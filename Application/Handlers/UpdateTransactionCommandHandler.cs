using MediatR;
using MySecureApi.Application.Commands;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Interfaces;

namespace MySecureApi.Application.Handlers;

public class UpdateTransactionCommandHandler
    : IRequestHandler<UpdateTransactionCommand, ApiResponse<bool>>
{
    private readonly ITransactionRepository _repo;

    public UpdateTransactionCommandHandler(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<bool>> Handle(UpdateTransactionCommand command, CancellationToken cancellationToken)
    {
        var trans = await _repo.GetByIdAsync(command.Id, command.UserId);

        if (trans == null)
        {
            return ApiResponse<bool>.ErrorResponse("Transaction not found or no permission");
        }

        trans.Amount = command.Dto.Amount;
        trans.Category = command.Dto.Category;
        trans.Description = command.Dto.Description;
        trans.Date = command.Dto.Date;

        await _repo.SaveChangesAsync();

        return ApiResponse<bool>.SuccessResponse(true, "Transaction updated successfully");
    }
}