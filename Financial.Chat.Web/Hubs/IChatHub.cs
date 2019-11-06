using Financial.Chat.Domain.HubModels;
using System.Threading.Tasks;

namespace Financial.Chat.Web.Hubs
{
    public interface IChatHub
    {
        Task Send(MessageModel message);

        Task OnConnectedAsync();
    }
}
