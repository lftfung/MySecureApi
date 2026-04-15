using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace Application.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return "Email already exists";
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name  = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.Password

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully!";
        }


    }
}
