using Microsoft.AspNetCore.Identity;
using System;

namespace Financial.Chat.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime? LastLoginDate { get; set; }
    }
}