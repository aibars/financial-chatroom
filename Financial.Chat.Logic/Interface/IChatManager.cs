using Financial.Chat.Domain.HubModels;
using System;
using System.Threading.Tasks;

namespace Financial.Chat.Logic.Interface
{
    public interface IChatManager
    {
        Task SaveMessage(Guid senderId, Guid receiverId, MessageModel message);
    }
}