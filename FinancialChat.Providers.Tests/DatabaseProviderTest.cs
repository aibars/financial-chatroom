using FinancialChat.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace FinancialChat.Providers.Tests
{
    public class DatabaseProviderTest : IDisposable
    {
        private readonly DatabaseProvider _databaseProvider;
        private readonly ApplicationDbContext _context;
        public DatabaseProviderTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(databaseName: "temp")
                      .Options;
            _context = new ApplicationDbContext(options);
            _databaseProvider =  new DatabaseProvider(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetMessagesTest()
        {
            // Arrange
            var fakeMessages = FakeData.GetMessages();
            _context.Users.AddRange(FakeData.GetUsers());
            _context.Messages.AddRange(fakeMessages);
            _context.SaveChanges();

            // Act
            var results = _databaseProvider.GetMessages().Result;

            // Assert
            Assert.Equal(26, results.Count);
            Assert.True(fakeMessages.OrderBy(x => x.SendDate).SequenceEqual(results));
        }

        [Fact]
        public void GetUserTest()
        {
            // Arrange
            _context.Users.AddRange(FakeData.GetUsers());
            _context.SaveChanges();
            var username = _context.Users.First().UserName;

            // Act
            var result = _databaseProvider.GetUser(username).Result;

            // Assert
            Assert.Equal(username, result.UserName);
        }

        [Fact]
        public async Task SaveMessageTest()
        {
            // Arrange
            _context.Users.AddRange(FakeData.GetUsers());
            _context.SaveChanges();
            var userId = _context.Users.First().Id;
            var message = "some message";
            // Act
            await _databaseProvider.SaveMessage(userId, message);
            var result = _context.Messages.First(x => x.SenderUserId == userId && x.Text.Equals(message));


            // Assert
            Assert.NotNull(result);
            Assert.Equal(message, result.Text);
        }
    }
}
