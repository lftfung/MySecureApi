using Domain;
using Microsoft.EntityFrameworkCore;
using MySecureApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MySecureApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) { 
            _context = context; 
        }

        public async Task<bool> IsEmailExistsAsync(string email) {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> AddAsync(User user) { 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        
        }

        public async Task<User?> GetByEmailAsync(string email) { 
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
