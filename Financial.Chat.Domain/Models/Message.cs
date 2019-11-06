using System;
using System.ComponentModel.DataAnnotations;

namespace Financial.Chat.Domain.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid SenderUserId { get; set; }

        public virtual ApplicationUser SenderUser { get; set; }

        public DateTime SendDate { get; set; }
    }
}