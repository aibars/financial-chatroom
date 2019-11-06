using Microsoft.AspNetCore.Identity;
using System;

namespace Financial.Chat.Domain.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime? LastLoginDate { get; set; }
    }
}