using Firebase.Database;
using Firebase.Database.Query;
using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firebase_API.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public GrupoRepository(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task<List<GrupoModel>> GetAllGrupos()
        {
            var grupos = await _firebaseClient
                .Child("grupos")
                .OnceAsync<GrupoModel>();

            return grupos.Select(g => new GrupoModel
            {
                Id = g.Key,
                NomeGrupo = g.Object.NomeGrupo,
                DescricaoGrupo = g.Object.DescricaoGrupo,
                HobbyId = g.Object.HobbyId,
                AdministradorId = g.Object.AdministradorId,
                DataCriacao = g.Object.DataCriacao,
                Membros = g.Object.Membros,
                Mensagens = g.Object.Mensagens
            }).ToList();
        }

        public async Task<GrupoModel> GetGrupoById(string id)
        {
            var grupo = await _firebaseClient
                .Child("grupos")
                .Child(id)
                .OnceSingleAsync<GrupoModel>();

            return grupo;
        }

        public async Task<GrupoModel> AddGrupo(GrupoModel grupo)
        {
            var result = await _firebaseClient
                .Child("grupos")
                .PostAsync(grupo);

            grupo.Id = result.Key;
            return grupo;
        }

        public async Task UpdateGrupo(GrupoModel grupo, string id)
        {
            await _firebaseClient
                .Child("grupos")
                .Child(id)
                .PutAsync(grupo);
        }

        public async Task DeleteGrupo(string id)
        {
            await _firebaseClient
                .Child("grupos")
                .Child(id)
                .DeleteAsync();
        }
    }
}
