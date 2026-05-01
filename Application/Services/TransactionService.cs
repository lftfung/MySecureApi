using MySecureApi.Application.DTOs;
using Domain;
using Domain.Interfaces;
using MySecureApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<AppTransaction> Create(CreateTransactionDto dto, Guid userId) {

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
            return transaction;
        
        }

        public async Task<List<AppTransaction>> GetAllByUserId(Guid userid) { 
            return await _repo.GetByUserIdAsync(userid);

        }

        public async Task<TransactionResponseDto?> GetById(Guid id, Guid userid) { 
            var t = await _repo.GetByIdAsync(id, userid);
            if (t == null) { 
                return null;

            }

            return new TransactionResponseDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Category = t.Category,
                Description = t.Description,
                Date = t.Date,
                UserName = t.User?.Name ?? "Unknown"
            };
        }

        public async Task<bool> Delete(Guid id, Guid userid) {
            var transaction = await _repo.GetByIdAsync(id, userid);

            if (transaction == null) {
                return false;
            }
        
            await _repo.DeleteAsync(transaction);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(Guid id, Guid userid, UpdateTransactionDto dto) {
            var trans = await _repo.GetByIdAsync(id, userid);
            if (trans == null) {
                return false;
            }
            
            trans.Amount = dto.Amount;
            trans.Category = dto.Category;
            trans.Description = dto.Description;
            trans.Date = dto.Date;

            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
