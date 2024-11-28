using Firebase.Database;
using Firebase.Database.Query;
using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firebase_API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public UsuarioRepository(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            var usuarios = await _firebaseClient
                .Child("Usuarios")
                .OnceAsync<UsuarioModel>();

            return usuarios.Select(u => new UsuarioModel
            {
                Id = u.Key,
                NameUsuario = u.Object.NameUsuario,
                NameSystemUsuario = u.Object.NameSystemUsuario,
                EmailUsuario = u.Object.EmailUsuario,
                SenhaUsuario = u.Object.SenhaUsuario,
                DateOfBirth = u.Object.DateOfBirth
            }).ToList();
        }

        public async Task<UsuarioModel> GetUsuarioById(string id)
        {
            var usuario = await _firebaseClient
                .Child("Usuarios")
                .Child(id)
                .OnceSingleAsync<UsuarioModel>();

            if (usuario != null)
            {
                usuario.Id = id;
            }

            return usuario;
        }

        public async Task<UsuarioModel> AddUsuario(UsuarioModel usuario)
        {
            var result = await _firebaseClient
                .Child("Usuarios")
                .PostAsync(usuario);

            usuario.Id = result.Key;
            return usuario;
        }

        public async Task UpdateUsuario(UsuarioModel usuario, string id)
        {
            await _firebaseClient
                .Child("Usuarios")
                .Child(id)
                .PutAsync(usuario);
        }

        public async Task DeleteUsuario(string id)
        {
            await _firebaseClient
                .Child("Usuarios")
                .Child(id)
                .DeleteAsync();
        }

        public async Task<UsuarioModel> GetUsuarioByEmailAndPassword(string email, string password)
        {
            // Recupera todos os usuários do banco de dados
            var usuarios = await _firebaseClient
                .Child("Usuarios")
                .OnceAsync<UsuarioModel>();

            // Procura por um usuário com o email e senha correspondentes
            var usuario = usuarios
                .Where(u => u.Object.EmailUsuario == email && u.Object.SenhaUsuario == password)
                .Select(u => new UsuarioModel
                {
                    Id = u.Key,
                    NameUsuario = u.Object.NameUsuario,
                    NameSystemUsuario = u.Object.NameSystemUsuario,
                    EmailUsuario = u.Object.EmailUsuario,
                    SenhaUsuario = u.Object.SenhaUsuario,
                    DateOfBirth = u.Object.DateOfBirth
                })
                .FirstOrDefault();

            return usuario;
        }

    }
}