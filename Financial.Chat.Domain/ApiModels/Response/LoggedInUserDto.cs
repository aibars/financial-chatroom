using System;
using System.Collections.Generic;
using System.Text;

namespace Financial.Chat.Domain.ApiModels.Response
{
    public class LoggedInUserDto
    {
        public string Email { get; set; }
        public long Expires { get; set; }
        public string Token { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
