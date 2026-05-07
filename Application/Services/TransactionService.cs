using MySecureApi.Application.DTOs;
using Domain;
using MySecureApi.Application.Interfaces;


namespace MySecureApi.Application.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly ITransactionAIService _aiService;

        public TransactionService(ITransactionRepository repo, ITransactionAIService aIService) {

            _repo = repo;
            _aiService = aIService;
        }

        public async Task<ApiResponse<TransactionResponseDto>> Create(CreateTransactionDto dto, Guid userId) {

            var category = dto.Category;
            if (string.IsNullOrWhiteSpace(category)) {
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

            var result = new TransactionResponseDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Category = transaction.Category,
                Description = transaction.Description,
                Date = transaction.Date,
                UserName = transaction.User?.Name ?? "Unknown"
            };
            return ApiResponse<TransactionResponseDto>.SuccessResponse(result);

        }

        public async Task<ApiResponse<List<TransactionResponseDto>>> GetAllByUserId(Guid userid) { 
            var domains = await _repo.GetByUserIdAsync(userid);

            List<TransactionResponseDto> results = new List<TransactionResponseDto>();
            foreach (var domain in domains) {
                results.Add(new TransactionResponseDto
                {
                    Id = domain.Id,
                    Amount = domain.Amount,
                    Category = domain.Category,
                    Description = domain.Description,
                    Date = domain.Date,
                    UserName = domain.User?.Name ?? "Unknown"
                });
            
            }
            return ApiResponse<List<TransactionResponseDto>>.SuccessResponse(results);
        }

        public async Task<ApiResponse<TransactionResponseDto>> GetById(Guid id, Guid userid) { 
            var t = await _repo.GetByIdAsync(id, userid);
            if (t == null) { 
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

        public async Task<ApiResponse<bool>> Delete(Guid id, Guid userid) {
            var transaction = await _repo.GetByIdAsync(id, userid);

            if (transaction == null) {
                return ApiResponse<bool>.ErrorResponse("Transaction not found or no permission");
            }
        
            await _repo.DeleteAsync(transaction);
            await _repo.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true);
        }

        public async Task<ApiResponse<bool>> Update(Guid id, Guid userid, UpdateTransactionDto dto) {
            var trans = await _repo.GetByIdAsync(id, userid);
            if (trans == null) {
                return ApiResponse<bool>.ErrorResponse("Transaction not found or no permission");
            }
            
            trans.Amount = dto.Amount;
            trans.Category = dto.Category;
            trans.Description = dto.Description;
            trans.Date = dto.Date;

            await _repo.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResponse(true);
        }
    }
}
