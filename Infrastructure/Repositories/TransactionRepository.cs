using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context) { 
            _context = context;
        }

        public async Task<AppTransaction> AddAsync(AppTransaction transaction) { 
            _context.Transactions.Add(transaction);
            return transaction;
        }

        public async Task<List<AppTransaction>> GetByUserIdAsync(Guid userId) { 
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<AppTransaction?> GetByIdAsync(Guid id, Guid userId) { 
            return await _context.Transactions
                .Include(t => t.Id)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<bool> DeleteAsync(AppTransaction transaction) { 
            _context.Transactions.Remove(transaction);
            return true;
        }

        public async Task SaveChangesAsync() { 
            await _context.SaveChangesAsync();

        }
    }
}
