using System.Text.RegularExpressions;
using Xunit;

namespace FinancialChat.Bot.Tests
{
    public class BotServiceTest
    {

        [Fact]
        public void ProcessCSVFromExternalServiceTest()
        {
            // Arrange
            var service = new BotService();
            //valid quote
            var quote = "aapl.us";
            var result1 = service.ProcessCSVFromExternalService(quote).Result;
            //invalid code
            var result2 = service.ProcessCSVFromExternalService("123").Result;

            // Assert
            Assert.Contains(quote.ToUpper(), result1);
            Assert.True(new Regex(@".+ quote is \$\d+(\.\d{1,2})").Match(result1).Success);
            Assert.Equal("There was an error processing the request.", result2);
        }
    }
}
