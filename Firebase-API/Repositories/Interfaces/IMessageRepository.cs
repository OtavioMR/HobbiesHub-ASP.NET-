using Firebase_API.Models;

namespace Firebase_API.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<bool> AddMessage(ChatMessageModel message);
        Task<List<ChatMessageModel>> GetMessagesByGroupId(string groupId);
    }

}
