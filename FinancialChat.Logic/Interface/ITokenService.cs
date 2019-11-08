using FinancialChat.Domain.Models;
using FinancialChat.Logic.Models;

namespace FinancialChat.Logic.Interface
{
    public interface ITokenService
    {
        JsonWebToken GenerateJwtToken(ApplicationUser user);
    }
}