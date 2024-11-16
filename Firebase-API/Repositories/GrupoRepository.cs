using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;

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
                .Child("Grupos")
                .OnceAsync<GrupoModel>();

            var result = new List<GrupoModel>();
            foreach (var grupo in grupos)
            {
                result.Add(grupo.Object);
            }
            return result;
        }

        public async Task<GrupoModel> GetGrupoById(string id)
        {
            var grupo = await _firebaseClient
                .Child("Grupos")
                .Child(id)
                .OnceSingleAsync<GrupoModel>();

            return grupo;
        }

        public async Task<GrupoModel> AddGrupo(GrupoModel grupo)
        {
            var result = await _firebaseClient
                .Child("Grupos")
                .PostAsync(grupo);

            grupo.Id = result.Key;
            return grupo;
        }

        public async Task UpdateGrupo(GrupoModel grupo, string id)
        {
            await _firebaseClient
                .Child("Grupos")
                .Child(id)
                .PutAsync(grupo);
        }

        public async Task DeleteGrupo(string id)
        {
            await _firebaseClient
                .Child("Grupos")
                .Child(id)
                .DeleteAsync();
        }
    }
}
