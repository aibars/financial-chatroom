using FinancialChat.Domain.Models;
using GenFu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialChat.Providers.Tests
{
    public static class FakeData
    {
        private static Guid Guid = Guid.NewGuid();
        public static List<Message> GetMessages()
        {
            var rnd = new Random();
            var messages = A.ListOf<Message>(26);
            var user = new ApplicationUser { Id = Guid };
            messages.ForEach(y => { y.Id = Guid.NewGuid(); y.SendDate = new DateTime(2019, 10, rnd.Next(1, 32)); y.SenderUserId = user.Id; });
            return messages.Select(_ => _).ToList();
        }

        public static List<ApplicationUser> GetUsers()
        {
            var users = A.ListOf<ApplicationUser>(5);
            users.ForEach(y =>  y.Id = Guid.NewGuid());
            users[0].Id = Guid;
            return users.Select(_ => _).ToList();
        }
    }
}
