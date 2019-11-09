using FinancialChat.Logic.Interface;
using FinancialChat.Providers.Interface;
using System;
using System.Threading.Tasks;

namespace FinancialChat.Logic
{
    /// <summary>
    /// Methods related to chat messages
    /// </summary>
    public class ChatManager : IChatManager
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IBotClient _botClient;

        public ChatManager(IDatabaseProvider databaseProvider, IBotClient botClient)
        {
            _databaseProvider = databaseProvider;
            _botClient = botClient;
        }

        /// <summary>
        /// Execute an RPC call to obtain a response from the bot service
        /// </summary>
        /// <param name="quote">A stock quote</param>
        /// <returns>A sentence that will be placed as a message comming from the Bot</returns>
        public string GetResponseFromBot(string quote)
        {
            var response = _botClient.Call(quote);

            _botClient.Close();
            return response;
        }

        /// <summary>
        /// Saves the message in the database when it is sent by a user
        /// </summary>
        /// <param name="message">The text input</param>
        /// <param name="username">The user's unique identifier</param>
        /// <returns>An awaitable task</returns>
        public async Task SaveMessage(string message, string username)
        {
            var user = await _databaseProvider.GetUser(username) ?? throw new ArgumentException("Error obtaining user from the database");
            
            await _databaseProvider.SaveMessage(user.Id, message);
        }

        /// <summary>
        /// Saves the message in the database when it is sent by the bot
        /// </summary>
        /// <param name="message">Bot Response</param>
        /// <returns>An awaitable task</returns>
        public async Task SaveMessage(string message)
        {
            await _databaseProvider.SaveMessage(message);
        }
    }
}