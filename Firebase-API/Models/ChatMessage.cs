namespace Models
{
    public class ChatMessage
    {
        public string GroupId { get; set; }  // Para identificar o grupo
        public string Sender { get; set; }   // Quem enviou
        public string Content { get; set; }  // O conteúdo da mensagem
        public DateTime Timestamp { get; set; }  // Quando a mensagem foi enviada
    }
}
