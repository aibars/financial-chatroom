using FinancialChat.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FinancialChat.Domain.Data
{
    /// <summary>
    /// EF Context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }
    }
}