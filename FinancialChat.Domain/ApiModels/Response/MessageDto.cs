using System;

namespace FinancialChat.Domain.ApiModels.Response
{
    public class MessageDto
    {
        public string Text { get; set; }

        public string UserName { get; set; }

        public DateTime SendDate { get; set; }
    }
}
