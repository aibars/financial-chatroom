using FinancialChat.Domain.HubModels;
using System;
using System.Threading.Tasks;

namespace FinancialChat.Logic.Interface
{
    public interface IChatManager
    {
        Task SaveMessage(MessageModel message, string username);
    }
}