using FinancialChat.Logic.Interface;
using FinancialChat.Providers.Interface;
using Moq;
using Xunit;

namespace FinancialChat.Logic.Tests
{
    public class ChatManagerTest
    {
        private ChatManager _chatManager;
        private Mock<IBotClient> _botClient;
        private Mock<IDatabaseProvider> _databaseProvider;

        public ChatManagerTest()
        {
            _botClient =  new Mock<IBotClient>();
            _databaseProvider = new Mock<IDatabaseProvider>();
            _chatManager = new ChatManager(_databaseProvider.Object, _botClient.Object);
        }

        [Fact]
        public void GetResponseFromBotTest()
        {
            // Arrange
            var msg = "response message";

            _botClient.Setup(x => x.Call(It.IsAny<string>())).Returns(msg);

            // Act
            var result = _chatManager.GetResponseFromBot("any");

            // Assert
            Assert.Equal(msg, result);
        }
    }
}