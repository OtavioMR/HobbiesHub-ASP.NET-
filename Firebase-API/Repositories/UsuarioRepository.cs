using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using System;
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

        // Método para adicionar um novo usuário
        public async Task<UsuarioModel> AddUsuario(UsuarioModel usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "O objeto de usuário não pode ser nulo.");
            }

            var result = await _firebaseClient
                .Child("Usuarios")
                .PostAsync(usuario);

            usuario.Id = result.Key;
            return usuario;
        }

        // Método para listar todos os usuários
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
                DateOfBirth = u.Object.DateOfBirth,
                // Adicione outros campos aqui, conforme necessário
            }).ToList();
        }

        // Método para obter um usuário pelo ID
        public async Task<UsuarioModel> GetUsuarioById(string id)
        {
            var usuario = await _firebaseClient
                .Child("Usuarios")
                .Child(id)
                .OnceSingleAsync<UsuarioModel>();

            if (usuario == null)
            {
                return null;
            }

            usuario.Id = id;
            return usuario;
        }

        // Método para atualizar um usuário existente pelo ID
        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario, string id)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario), "O objeto de usuário não pode ser nulo.");
            }

            var existingUsuario = await GetUsuarioById(id);

            if (existingUsuario == null)
            {
                throw new KeyNotFoundException($"Usuário com ID: {id} não encontrado no Firebase.");
            }

            usuario.Id = id;
            await _firebaseClient
                .Child("Usuarios")
                .Child(id)
                .PutAsync(usuario);

            return usuario;
        }

        // Método para deletar um usuário pelo ID
        public async Task<bool> DeleteUsuario(string id)
        {
            var existingUsuario = await GetUsuarioById(id);

            if (existingUsuario == null)
            {
                throw new KeyNotFoundException($"Usuário com ID: {id} não encontrado no Firebase.");
            }

            await _firebaseClient
                .Child("Usuarios")
                .Child(id)
                .DeleteAsync();

            return true;
        }
    }
}
