using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace MySecureApi.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<AppTransaction> AddAsync(AppTransaction appTransaction);
        Task<List<AppTransaction>> GetByUserIdAsync(Guid userId);
        Task<AppTransaction?> GetByIdAsync(Guid id, Guid userId);
        Task<bool> DeleteAsync(AppTransaction transaction);
        Task SaveChangesAsync();

    }
}
