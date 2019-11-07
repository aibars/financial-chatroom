using FinancialChat.Domain.HubModels;
using FinancialChat.Domain.Models;
using System;
using System.Threading.Tasks;

namespace FinancialChat.Providers.Interface
{
    public interface IDatabaseProvider
    {
        Task<ApplicationUser> GetUser(string username);

        Task SaveMessage(Guid senderId, MessageModel message);
    }
}