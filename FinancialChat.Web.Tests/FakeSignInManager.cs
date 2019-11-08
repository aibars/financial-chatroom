using FinancialChat.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.Web.Tests
{
    public class FakeSignInManager : SignInManager<ApplicationUser>
    {
        private SignInResult signInResult;
        public FakeSignInManager(Mock<IUserStore<ApplicationUser>> mockUserStore, IQueryable<ApplicationUser> users, SignInResult fakeSignInResult)
                : base(new FakeUserManager(mockUserStore, users),
                     new Mock<IHttpContextAccessor>().Object,
                     new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                     new Mock<IOptions<IdentityOptions>>().Object,
                     new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                     new Mock<IAuthenticationSchemeProvider>().Object,
                     new Mock<IUserConfirmation<ApplicationUser>>().Object)
        { signInResult = fakeSignInResult; }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(signInResult);
        }

        public override Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            return Task.FromResult(true);
        }
    }
}