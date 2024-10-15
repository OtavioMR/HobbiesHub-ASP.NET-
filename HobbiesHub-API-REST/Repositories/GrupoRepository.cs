using HobbiesHub_API_REST.Data;
using HobbiesHub_API_REST.Models;
using HobbiesHub_API_REST.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HobbiesHub_API_REST.Repositories
{
    public class GrupoRepository : IGrupoRepository
    { 
        private readonly HobbiesHubSystemDbContext _dbContext;

        public GrupoRepository(HobbiesHubSystemDbContext hobbiesHubSystemDbContext)
        {
            _dbContext = hobbiesHubSystemDbContext;
        }

        // Método para adicionar um grupo
        public async Task<GrupoModel> AddGrupo(GrupoModel grupo)
        {
            await _dbContext.Grupos.AddAsync(grupo);
            await _dbContext.SaveChangesAsync();
            return grupo;
        }


        //Método para excluir um grupo
        public async Task<bool> DeleteGrupo(int id)
        {
            GrupoModel grupoById = await GetGrupoById(id);

            if (grupoById == null)
            {
                return false; // Retorna false se não encontrar o grupo
            }

            _dbContext.Grupos.Remove(grupoById);
            await _dbContext.SaveChangesAsync();
            return true; // Retorna true se o grupo for excluído
        }

        //Método para buscar todos os grupos
        public async Task<List<GrupoModel>> GetAllGrupos()
        {
            return await _dbContext.Grupos.ToListAsync();
        }


        //Método para buscar o grupo pelo Id específico
        public async Task<GrupoModel> GetGrupoById(int id)
        {
            return await _dbContext.Grupos.FirstOrDefaultAsync(x => x.Id == id);
        }

        //Método para atualizar o grupo pelo Id específico
        public async Task<GrupoModel> UpdateGrupo(GrupoModel grupo, int id)
        {
            GrupoModel grupoById = await GetGrupoById(id);
            if (grupoById == null)
            {
                throw new Exception($"Grupo com o ID: {id} não foi encontrado no Banco de Dados.");
            }

            // Elementos que serão atualizados
            grupoById.NameGrupo = grupo.NameGrupo;
            grupoById.CategoryGrupo = grupo.CategoryGrupo;
            grupoById.DescriptionGrupo = grupo.DescriptionGrupo;
            grupoById.LimiteUsuariosGrupo = grupo.LimiteUsuariosGrupo;

            _dbContext.Grupos.Update(grupoById);
            await _dbContext.SaveChangesAsync();

            return grupoById; // Retorna o usuario atualizado
        }
    }
}
