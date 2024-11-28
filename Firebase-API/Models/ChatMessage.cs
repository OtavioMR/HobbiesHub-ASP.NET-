using System;

namespace Firebase_API.Models
{
    public class ChatMessageModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Gerar automaticamente se não presente
        public string GroupId { get; set; }
        public string UserId { get; set; }
        public string Texto { get; set; }
        public DateTime Hora { get; set; }
    }
}
