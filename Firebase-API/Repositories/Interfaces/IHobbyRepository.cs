using Firebase_API.Models;

namespace Firebase_API.Repositories.Interfaces
{
    public interface IHobbyRepository
    {
        Task<List<HobbyModel>> GetAllHobbies();
        Task<HobbyModel?> GetHobbiesById(string id);  // Alterado para permitir retorno nulo
        Task<HobbyModel> AddHobby(HobbyModel model);
        Task<HobbyModel> UpdateHobby(HobbyModel model, string id);
        Task<bool> DeleteHobby(string id);
    }
}
