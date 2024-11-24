using Firebase_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firebase_API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> GetAllUsuarios();
        Task<UsuarioModel> GetUsuarioById(string id);
        Task<UsuarioModel> GetUsuarioByEmailAndPassword(string email, string password);
        Task<UsuarioModel> AddUsuario(UsuarioModel usuario);
        Task UpdateUsuario(UsuarioModel usuario, string id);
        Task DeleteUsuario(string id);
    }
}
