using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;

public class MessageRepository : IMessageRepository
{
    // Aqui você pode integrar com o banco de dados ou Firebase
    private readonly List<ChatMessageModel> _messages = new List<ChatMessageModel>();

    public async Task<bool> AddMessage(ChatMessageModel message)
    {
        // Aqui você salvaria a mensagem no banco ou no Firebase
        _messages.Add(message);
        return await Task.FromResult(true); // Simulando sucesso
    }

    public async Task<List<ChatMessageModel>> GetMessagesByGroupId(string groupId)
    {
        // Aqui você faria a busca das mensagens por grupo no banco de dados
        var result = _messages.Where(m => m.GroupId == groupId).ToList();
        return await Task.FromResult(result);
    }
}
