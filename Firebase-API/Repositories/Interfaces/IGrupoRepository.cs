using Firebase_API.Models;

namespace Firebase_API.Repositories.Interfaces
{
    public interface IGrupoRepository
    {
        Task<List<GrupoModel>> GetAllGrupos();
        Task<GrupoModel> GetGrupoById(string id);
        Task<GrupoModel> AddGrupo(GrupoModel model);
        Task<GrupoModel> UpdateGrupo(GrupoModel model, string id );
        Task<bool> DeleteGrupo (string id);
    }
}
