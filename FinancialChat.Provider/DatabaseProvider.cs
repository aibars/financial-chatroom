using FinancialChat.Domain.Data;
using FinancialChat.Domain.Models;
using FinancialChat.Providers.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FinancialChat.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        protected readonly ApplicationDbContext _context;
        public DatabaseProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUser(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task SaveMessage(Guid senderId, string message)
        {
            await _context.Messages.AddAsync(new Message
            {
                Text = message,
                SendDate = DateTime.UtcNow,
                SenderUserId = senderId
            });

            await _context.SaveChangesAsync();
        }       
    }
}
