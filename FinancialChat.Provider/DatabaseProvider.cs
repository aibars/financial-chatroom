using System;
using System.Threading.Tasks;
using FinancialChat.Domain.Data;
using FinancialChat.Domain.HubModels;
using FinancialChat.Domain.Models;
using FinancialChat.Providers.Interface;
using Microsoft.EntityFrameworkCore;

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

        public async Task SaveMessage(Guid senderId, MessageModel message)
        {
            await _context.Messages.AddAsync(new Message
            {
                Text = message.Message,
                SendDate = message.SendDate,
                SenderUserId = senderId
            });

            await _context.SaveChangesAsync();
        }       
    }
}
