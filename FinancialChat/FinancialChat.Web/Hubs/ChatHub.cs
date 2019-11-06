using FinancialChat.Domain.HubModels;
using FinancialChat.Domain.Models;
using FinancialChat.Logic.Interface;
using FinancialChat.Providers.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace FinancialChat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub, IChatHub
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly IChatManager _chatManager;

        public ChatHub(UserManager<ApplicationUser> userManager,
            IDatabaseProvider databaseProvider,
            IChatManager chatManager)
        {
            _userManager = userManager;
            _chatManager = chatManager;
        }

        /// <summary>
        /// Add the logged in User to the room. 
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("User is not logged in.");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "chatroom");
                await base.OnConnectedAsync();
            }
        }

        /// <summary>
        /// Saves the message into the db and sends it via the SignalR socket
        /// </summary>
        public async Task Send(MessageModel message)
        {
            var username = Context.User.Identity.Name;

            message.SendDate = DateTime.UtcNow;

            if(!message.Message.Contains(@"/stock=")) {
                await Clients.Group("chatroom").SendAsync("SendMessage", message);
                await _chatManager.SaveMessage( message, username);
            }
            else
            {
                await _chatManager.SaveMessage(message, username);
                await Clients.Group("chatroom").SendAsync("SendMessage", message);
            }
        }
    }
}
