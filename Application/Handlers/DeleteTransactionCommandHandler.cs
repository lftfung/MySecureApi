using MediatR;
using MySecureApi.Application.Commands;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Interfaces;

namespace MySecureApi.Application.Handlers;

public class DeleteTransactionCommandHandler
    : IRequestHandler<DeleteTransactionCommand, ApiResponse<bool>>
{
    private readonly ITransactionRepository _repo;

    public DeleteTransactionCommandHandler(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteTransactionCommand command, CancellationToken cancellationToken)
    {
        var transaction = await _repo.GetByIdAsync(command.Id, command.UserId);

        if (transaction == null)
        {
            return ApiResponse<bool>.ErrorResponse("Transaction not found or no permission");
        }

        await _repo.DeleteAsync(transaction);
        await _repo.SaveChangesAsync();

        return ApiResponse<bool>.SuccessResponse(true, "Transaction deleted successfully");
    }
}