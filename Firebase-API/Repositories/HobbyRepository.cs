using Firebase.Database;
using Firebase.Database.Query;
using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;

namespace Firebase_API.Repositories
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public HobbyRepository(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        // Método para adicionar um novo hobby
        public async Task<HobbyModel> AddHobby(HobbyModel hobby)
        {
            if (hobby == null)
            {
                throw new ArgumentNullException(nameof(hobby), "O objeto de hobby não pode ser nulo.");
            }

            var result = await _firebaseClient
                .Child("Hobbies")
                .PostAsync(hobby);

            hobby.Id = result.Key;

            if (string.IsNullOrEmpty(hobby.Id))
            {
                throw new InvalidOperationException("Erro ao gerar o ID para o hobby.");
            }

            return hobby;
        }

        // Método para deletar um hobby pelo ID
        public async Task<bool> DeleteHobby(string id)
        {
            var existingHobby = await GetHobbiesById(id);

            if (existingHobby == null)
            {
                throw new KeyNotFoundException($"Hobby com o ID: {id} não consta no banco de dados.");
            }

            await _firebaseClient
                .Child("Hobbies")
                .Child(id)
                .DeleteAsync();

            return true;
        }

        // Método para listar todos os hobbies
        public async Task<List<HobbyModel>> GetAllHobbies()
        {
            var hobbies = await _firebaseClient
                .Child("Hobbies")
                .OnceAsync<HobbyModel>();

            return hobbies.Select(u => new HobbyModel
            {
                Id = u.Key,
                NameHobby = u.Object.NameHobby,
                DescriptionHobby = u.Object.DescriptionHobby,
            }).ToList();
        }

        // Método para obter um hobby pelo ID
        public async Task<HobbyModel?> GetHobbiesById(string id)
        {
            var hobby = await _firebaseClient
                .Child("Hobbies")
                .Child(id)
                .OnceSingleAsync<HobbyModel>();

            return hobby != null ? hobby : null; // Retorna o hobby ou null
        }

        // Método para atualizar um hobby existente pelo ID
        public async Task<HobbyModel> UpdateHobby(HobbyModel hobby, string id)
        {
            if (hobby == null)
            {
                throw new ArgumentNullException(nameof(hobby), "O objeto de hobby não pode ser nulo.");
            }

            var existingHobby = await GetHobbiesById(id);

            if (existingHobby == null)
            {
                throw new KeyNotFoundException($"Hobby com o ID: {id} não foi encontrado no banco de dados.");
            }

            hobby.Id = id;
            await _firebaseClient
                .Child("Hobbies")
                .Child(id)
                .PatchAsync(hobby);

            return hobby;
        }
    }
}
