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

        public async Task<AppTransaction?> GetById(Guid id, Guid userid) { 
            return await _context.Transactions
                .Include(t => t.user)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userid);
        } 
    }
}
