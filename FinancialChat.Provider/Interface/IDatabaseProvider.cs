using FinancialChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialChat.Providers.Interface
{
    public interface IDatabaseProvider
    {
        Task<ApplicationUser> GetUser(string username);

        Task SaveMessage(Guid senderId, string message);

        Task<List<Message>> GetMessages();
    }
}