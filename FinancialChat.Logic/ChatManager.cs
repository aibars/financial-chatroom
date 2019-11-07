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
        private readonly IBotClient _botClient;

        public ChatManager(IDatabaseProvider databaseProvider, IBotClient botClient)
        {
            _databaseProvider = databaseProvider;
            _botClient = botClient;
        }

        public string GetResponseFromBot(string quote)
        {
            var response = _botClient.Call(quote);

            _botClient.Close();
            return response;
        }

        public async Task SaveMessage(string message, string username)
        {
            var user = await _databaseProvider.GetUser(username) ?? throw new ArgumentException("Error obtaining user from the database");
            
            await _databaseProvider.SaveMessage(user.Id, message);
        }
    }
}