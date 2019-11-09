using FinancialChat.Domain.HubModels;
using System.Threading.Tasks;

namespace FinancialChat.Logic.Interface
{
    public interface IChatManager
    {
        Task SaveMessage(string message, string username);

        Task SaveMessage(string message);

        string GetResponseFromBot(string quote); 
    }
}