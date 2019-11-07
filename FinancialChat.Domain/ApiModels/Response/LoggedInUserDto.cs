using System;

namespace FinancialChat.Domain.ApiModels.Response
{
    public class LoggedInUserDto
    {
        public string UserName { get; set; }
        public long Expires { get; set; }
        public string Token { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
