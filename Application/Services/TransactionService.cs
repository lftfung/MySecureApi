using Application.DTOs;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransactionService
    {
        private readonly AppDbContext _context;
        public TransactionService(AppDbContext context) { 
            _context = context;
        }

        public async Task<AppTransaction> Create(CreateTransactionDto dto, Guid userId) {
            var transaction = new AppTransaction
            {
                Amount = dto.Amount,
                Category = dto.Category,
                Description = dto.Description,
                Date = dto.Date,
                UserId = userId
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        
        }

        public async Task<List<AppTransaction>> GetAllByUserId(Guid userid) {
            return await _context.Transactions
                    .Where(t => t.UserId == userid)
                    .OrderByDescending(t => t.Date)
                    .ToListAsync();
        }

        public async Task<TransactionResponseDto?> GetById(Guid id, Guid userid) { 
            var t = await _context.Transactions
                .Include(t => t.user)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userid);

            if (t == null)
            {
                return null;
            }

            return new TransactionResponseDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Category = t.Category,
                Description = t.Description,
                Date = t.Date,
                UserName = t.user?.Name ?? "Unknown"

            };
        }

        public async Task<bool> Delete(Guid id, Guid userid) {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userid);

            if (transaction == null) {
                return false;

            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        
        }

        public async Task<bool> Update(Guid id, Guid userid, UpdateTransactionDto dto) { 
            var trans = await _context.Transactions
                .FirstOrDefaultAsync(t=> t.Id == id && t.UserId == userid);

            if (trans == null)
            {
                return false;
            }
            
            trans.Amount = dto.Amount;
            trans.Category = dto.Category;
            trans.Description = dto.Description;
            trans .Date = dto.Date; 

            await _context.SaveChangesAsync();
            return true;

        }
    }
}
