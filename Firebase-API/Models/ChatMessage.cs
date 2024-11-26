using System;

namespace Firebase_API.Models
{
    public class ChatMessageModel
    {
        public string Id { get; set; }
        public string GroupId { get; set; }
        public string UserId { get; set; }
        public string Texto { get; set; } // Conteúdo da mensagem
        public DateTime Hora { get; set; }
    }
}
