using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecureApi.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task<User> AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
