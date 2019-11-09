using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialChat.Domain.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid? SenderUserId { get; set; }

        public virtual ApplicationUser SenderUser { get; set; }

        public DateTime SendDate { get; set; }

        public string SenderBot { get; set; }
    }
}