using Firebase.Database;
using System;

namespace FirebaseServicosAPI.Services
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            // Substitua pela URL do seu Firebase Realtime Database
            string firebaseDatabaseUrl = "https://your-firebase-database-url.firebaseio.com/";

            if (string.IsNullOrEmpty(firebaseDatabaseUrl))
            {
                throw new ArgumentNullException(nameof(firebaseDatabaseUrl), "A URL do Firebase não pode estar vazia.");
            }

            // Inicializa o cliente Firebase
            _firebaseClient = new FirebaseClient(firebaseDatabaseUrl);
        }

        // Método GetClient para retornar a instância do cliente Firebase
        public FirebaseClient GetClient()
        {
            return _firebaseClient;
        }
    }
}
