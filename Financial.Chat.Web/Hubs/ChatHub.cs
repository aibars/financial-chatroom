using Financial.Chat.Domain.HubModels;
using Financial.Chat.Domain.Models;
using Financial.Chat.Logic.Interface;
using Financial.Chat.Providers.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Financial.Chat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub, IChatHub
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IChatManager _chatManager;

        public ChatHub(UserManager<ApplicationUser> userManager,
            IDatabaseProvider databaseProvider,
            IChatManager chatManager)
        {
            _userManager = userManager;
            _databaseProvider = databaseProvider;
            _chatManager = chatManager;
        }

        public override async Task OnConnectedAsync()
        {
            //TODO: support multiroom
            var name = Context.User.Identity.Name;
            if (string.IsNullOrEmpty(name))
            {
                await base.OnConnectedAsync();
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, name);
                await base.OnConnectedAsync();
            }
        }

        public async Task Send(MessageModel message)
        {
            throw new NotImplementedException();
        }
    }
}
