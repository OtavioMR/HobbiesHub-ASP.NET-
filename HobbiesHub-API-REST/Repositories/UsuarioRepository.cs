using HobbiesHub_API_REST.Data;
using HobbiesHub_API_REST.Models;
using HobbiesHub_API_REST.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HobbiesHub_API_REST.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HobbiesHubSystemDbContext _dbContext;

        public UsuarioRepository(HobbiesHubSystemDbContext hobbiesHubSystemDbContext)
        {
            _dbContext = hobbiesHubSystemDbContext;
        }

        // Método para adicionar usuário
        public async Task<UsuarioModel> AddUsuario(UsuarioModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario;
        }

        // Método para deletar um usuário
        public async Task<bool> DeleteUsuario(int id)
        {
            UsuarioModel usuarioById = await GetUsuarioById(id);

            if (usuarioById == null)
            {
                throw new Exception($"Usuário com o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Usuarios.Remove(usuarioById);
            await _dbContext.SaveChangesAsync();
            return true; // Retorna true se a exclusão for bem-sucedida
        }

        // Método para listar todos os usuários
        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        // Método para listar os usuários pelo Id
        public async Task<UsuarioModel> GetUsuarioById(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Método para atualizar um usuário
        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioById = await GetUsuarioById(id);
            if (usuarioById == null)
            {
                throw new Exception($"Usuário com o ID: {id} não foi encontrado no Banco de Dados.");
            }

            // Elementos que serão atualizados
            usuarioById.UsuarioName = usuario.UsuarioName;
            usuarioById.UsuarioEmail = usuario.UsuarioEmail;
            usuarioById.UsuarioNameSystem = usuario.UsuarioNameSystem;
            usuarioById.UsuarioSenhaHash = usuario.UsuarioSenhaHash;
            usuarioById.UsuarioAge = usuario.UsuarioAge;
            usuarioById.UsuarioDateCadastro = usuario.UsuarioDateCadastro;

            _dbContext.Usuarios.Update(usuarioById);
            await _dbContext.SaveChangesAsync();

            return usuarioById; // Retorna o usuário atualizado
        }
    }
}
