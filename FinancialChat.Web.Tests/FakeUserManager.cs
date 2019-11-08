using FinancialChat.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialChat.Web.Tests
{
    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager(Mock<IUserStore<ApplicationUser>> mockUserStore, IQueryable<ApplicationUser> users)
            : base(mockUserStore.Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<ApplicationUser>>().Object,
              new IUserValidator<ApplicationUser>[0],
              new IPasswordValidator<ApplicationUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<ApplicationUser>>>().Object)
        {
            Users = users;
        }

        public override Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override IQueryable<ApplicationUser> Users { get; }
    }
}
