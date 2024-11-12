using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;

namespace Firebase_API.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public GrupoRepository(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }


        // Método para criar um novo grupo
        public async Task<GrupoModel> AddGrupo(GrupoModel grupo)
        {
            if (grupo == null)
            {
                throw new ArgumentNullException(nameof(grupo), "O objeto de grupo não pode ser nulo");
            }

            var result = await _firebaseClient
                .Child("Grupos")
                .PostAsync(grupo);

            grupo.Id = result.Key;
            return grupo;
        }


        // Método para deletar um grupo por ID
        public async Task<bool> DeleteGrupo(string id)
        {
            var existingGrupon = await GetGrupoById(id);

            if (existingGrupon == null)
            {
                throw new KeyNotFoundException($"Grupo com o ID: {id} não foi encontrado no banco de dados,");
            }

            await _firebaseClient
                .Child("grupos")
                .Child(id)
                .DeleteAsync();

            return true;
        }


        // Método para listar todos os grupos
        public async Task<List<GrupoModel>> GetAllGrupos()
        {
            var grupos = await _firebaseClient
               .Child("Grupos")
               .OnceAsync<GrupoModel>();

            return grupos.Select(u => new GrupoModel
            {
                Id = u.Key,

                NomeGrupo = u.Object.NomeGrupo,
                DescricaoGrupo = u.Object.DescricaoGrupo,
                CategoriaGrupo = u.Object.CategoriaGrupo,
                // Adicione outros campos aqui, conforme necessário
            }).ToList();
        }


        // Método para listar um grupo por ID
        public async Task<GrupoModel> GetGrupoById(string id)
        {
            var grupo = await _firebaseClient
                .Child("Grupos")
                .Child(id)
                .OnceSingleAsync<GrupoModel>();

            if (grupo == null)
            {
                return null;
            }

            grupo.Id = id;
            return grupo;
        }

        public async Task<GrupoModel> UpdateGrupo(GrupoModel grupo, string id)
        {
            if (grupo == null)
            {
                throw new ArgumentNullException(nameof(grupo), "O objeto de grupo não pode ser nulo.");
            }

            var existingGrupo = await GetGrupoById(id);

            if (existingGrupo == null)
            {
                throw new KeyNotFoundException($"Grupo com ID: {id} não encontrado no Firebase.");
            }

            grupo.Id = id;
            await _firebaseClient
                .Child("Grupos")
                .Child(id)
                .PutAsync(grupo);

            return grupo;
        }
    }
}
