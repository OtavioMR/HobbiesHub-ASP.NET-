using HobbiesHub_API_REST.Models;

namespace HobbiesHub_API_REST.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioModel> AddUsuario(UsuarioModel model);
        Task<List<UsuarioModel>> GetAllUsuarios();
        Task<UsuarioModel> GetUsuarioById(int id);
        Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario, int id);
        Task<bool> DeleteUsuario(int id);
    }
}
