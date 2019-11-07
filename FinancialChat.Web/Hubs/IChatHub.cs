using FinancialChat.Domain.HubModels;
using System.Threading.Tasks;

namespace FinancialChat.Web.Hubs
{
    public interface IChatHub
    {
        Task Send(MessageModel message);

        Task OnConnectedAsync();
    }
}
