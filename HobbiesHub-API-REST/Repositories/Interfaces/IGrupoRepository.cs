using HobbiesHub_API_REST.Models;

namespace HobbiesHub_API_REST.Repositories.Interfaces
{
    public interface IGrupoRepository
    {
        Task<GrupoModel> AddGrupo(GrupoModel model);
        Task<List<GrupoModel>> GetAllGrupos();
        Task<GrupoModel> GetGrupoById(int id);
        Task<GrupoModel> UpdateGrupo(GrupoModel grupo, int id);
        Task<bool> DeleteGrupo(int id);
    }
}
