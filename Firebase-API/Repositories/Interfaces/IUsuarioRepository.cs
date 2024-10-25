using Firebase_API.Models;

namespace Firebase_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> GetAllUsuarios();
        Task<UsuarioModel> GetUsuarioById(string id);
        Task<UsuarioModel> AddUsuario(UsuarioModel model);
        Task<UsuarioModel> UpdateUsuario(UsuarioModel model, string id);
        Task<bool> DeleteUsuario(string id);
    }
}
