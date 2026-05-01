using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace MySecureApi.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AppTransaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity => {

                entity.HasKey(u => u.Id);
                entity.Property(u=>u.Email).IsRequired().HasMaxLength(256);
                entity.Property(u=>u.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => u.Email).IsUnique();
            
            }); 

            modelBuilder.Entity<AppTransaction>(entity => { 
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Amount).HasPrecision(18, 2);
                entity.Property(t => t.Category).HasMaxLength(50);
                entity.Property(t => t.Description).HasMaxLength(500);

                entity.HasOne(t => t.User)
                    .WithMany()
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(t => t.UserId);
            }
                
     
            );

            modelBuilder.Entity<AppTransaction>().Property(t => t.Amount).HasPrecision(18, 2);
        }
    }
}
