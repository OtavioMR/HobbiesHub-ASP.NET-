using Firebase_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firebase_API.Repositories.Interfaces
{
    public interface IGrupoRepository
    {
        Task<List<GrupoModel>> GetAllGrupos();
        Task<GrupoModel> GetGrupoById(string id);
        Task<GrupoModel> AddGrupo(GrupoModel grupo);
        Task UpdateGrupo(GrupoModel grupo, string id);
        Task DeleteGrupo(string id);
    }
}
