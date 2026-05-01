using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using MySecureApi.Application.DTOs;
using Domain;
using MySecureApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt; 
using Microsoft.IdentityModel.Tokens;    


using BC = BCrypt.Net.BCrypt;
namespace MySecureApi.Application.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return "Email already exists";
            }

            string passwordHash = BC.HashPassword(dto.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name  = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully!";
        }

        public async Task<string> Login(LoginDto dto) { 
        
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                return "User not Found";
            }

            bool isPasswordValid = BC.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return "Invalid password";
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

           

         
        }
    }
}
