using FinancialChat.Domain.HubModels;
using System.Threading.Tasks;

namespace FinancialChat.Web.Hubs
{
    public interface IChatHub
    {
        Task Send(string message);

        Task OnConnectedAsync();
    }
}
