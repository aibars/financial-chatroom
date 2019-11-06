using Financial.Chat.Domain.Models;
using Financial.Chat.Logic.Models;

namespace Financial.Chat.Logic.Interface
{
    public interface ITokenService
    {
        JsonWebToken GenerateJwtToken(string email, ApplicationUser user);
    }
}