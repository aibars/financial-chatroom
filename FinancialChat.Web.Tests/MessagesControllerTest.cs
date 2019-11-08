using AutoMapper;
using FinancialChat.Domain.ApiModels.Response;
using FinancialChat.Domain.Models;
using FinancialChat.Providers.Interface;
using FinancialChat.Web.Controllers;
using GenFu;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FinancialChat.Web.Tests
{
    public class MessagesControllerTest
    {
        [Fact]
        public void WhenGetMessagesReturnsAll()
        {
            // Arrange
            var service = new Mock<IDatabaseProvider>();
            var mapper = new Mock<IMapper>();

            var messages = GetFakeData();
            service.Setup(x => x.GetMessages()).Returns(Task.FromResult(messages));
            var messageModels = GetModels();
            mapper.Setup(m => m.Map<List<MessageDto>>(It.IsAny<List<Message>>())).Returns(messageModels);
            var controller = new MessagesController(service.Object, mapper.Object);

            // Act
            var results = controller.GetRoomMessages().Result;

            var count = results.Count();

            // Assert
            Assert.Equal(26, count);
        }

        private List<Message> GetFakeData()
        {
            var messages = A.ListOf<Message>(26);
            messages.ForEach(y => y.Id = Guid.NewGuid());
            return messages.Select(_ => _).ToList();
        }

        private List<MessageDto> GetModels()
        {
            var messages = A.ListOf<MessageDto>(26);
            return messages.Select(_ => _).ToList();
        }
    }
}