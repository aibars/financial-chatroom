using FinancialChat.Domain.Models;
using FinancialChat.Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinancialChat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub, IChatHub
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly IChatManager _chatManager;

        public ChatHub(UserManager<ApplicationUser> userManager,
            IChatManager chatManager)
        {
            _userManager = userManager;
            _chatManager = chatManager;
        }

        /// <summary>
        /// Saves the message into the db and sends it via the SignalR socket
        /// </summary>
        public async Task Send(string message)
        {
            var username = Context.User.Identity.Name;

            if (!message.Contains(@"/stock="))
            {
                await Clients.All.SendAsync("sendToAll", username, message);
                await _chatManager.SaveMessage(message, username);
            }
            else
            {
                var quote = new Regex("/stock=(.+)").Match(message).Groups[0].Value;

                var quoteResponse = _chatManager.GetResponseFromBot(quote);

                await Clients.Group("chatroom").SendAsync("sendToAllFromBot", quoteResponse);
            }
        }
    }
}
