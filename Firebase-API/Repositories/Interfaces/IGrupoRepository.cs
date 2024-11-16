using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase_API.Models;

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
