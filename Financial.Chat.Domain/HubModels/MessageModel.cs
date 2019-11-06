using System;
using System.ComponentModel.DataAnnotations;

namespace Financial.Chat.Domain.HubModels
{
    public class MessageModel
    {
        [Required]
        public string Message { get; set; }

        public DateTime SendDate { get; set; }
    }
}