using Financial.Chat.Domain.HubModels;
using Financial.Chat.Logic.Interface;
using System;
using System.Threading.Tasks;

namespace Financial.Chat.Logic
{
    public class ChatManager : IChatManager
    {
        public async Task SaveMessage(Guid senderId, Guid receiverId, MessageModel message)
        {
            throw new NotImplementedException();
        }
    }
}