using FinancialChat.Domain.Data;
using FinancialChat.Domain.Models;
using FinancialChat.Providers.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace FinancialChat.Providers
{
    /// <summary>
    /// Methods for accessing the database
    /// </summary>
    public class DatabaseProvider : IDatabaseProvider
    {
        protected readonly ApplicationDbContext _context;
        public DatabaseProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtains all Messages, ordered by send data and including the associated user.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Message>> GetMessages()
        {
            return await _context.Messages.Include(y => y.SenderUser).OrderBy(x => x.SendDate).ToListAsync();
        }

        /// <summary>
        /// Obtains a user by username
        /// </summary>
        public async Task<ApplicationUser> GetUser(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        /// <summary>
        /// Saves a message after it has been posted in the chatroom
        /// </summary>
        public async Task SaveMessage(Guid senderId, string message)
        {
            //1. Count number of saved messages
            if (await _context.Messages.CountAsync() >= 50)
            {
                //2. Get the oldest message and remove it
                var oldestMsg = (await _context.Messages.ToListAsync()).OrderBy(x => x.SendDate).First();
                _context.Messages.Remove(oldestMsg);
                await _context.SaveChangesAsync();
            }

            await _context.Messages.AddAsync(new Message
            {
                Text = message,
                SendDate = DateTime.Now,
                SenderUserId = senderId
            });

            await _context.SaveChangesAsync();
        }
    }
}
