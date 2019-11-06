using FinancialChat.Domain.HubModels;
using FinancialChat.Logic.Interface;
using FinancialChat.Providers.Interface;
using System;
using System.Threading.Tasks;

namespace FinancialChat.Logic
{
    public class ChatManager : IChatManager
    {
        private readonly IDatabaseProvider _databaseProvider;

        public ChatManager(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public Task GetResponseFromBotAndSaveMessage(MessageModel message)
        {
            throw new NotImplementedException();
        }

        public async Task SaveMessage(MessageModel message, string username = "bot")
        {
            if (username != "bot")
            {
                var user = await _databaseProvider.GetUser(username) ?? throw new ArgumentException("Error obtaining user from the database");
                await _databaseProvider.SaveMessage(user.Id, message);
            }
            else
            {
                await _databaseProvider.SaveMessage(message);
            }
        }
    }
}