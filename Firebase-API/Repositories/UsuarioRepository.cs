using Firebase_API.Models;
using Firebase_API.Data;
using Firebase_API.Repositories.Interfaces;
using Firebase_API.Repositories;
using Firebase.Database.Query;
using Firebase.Database;

namespace Firebase_API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task<UsuarioModel> AddUsuario(UsuarioModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUsuario(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioModel> GetUsuarioById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
