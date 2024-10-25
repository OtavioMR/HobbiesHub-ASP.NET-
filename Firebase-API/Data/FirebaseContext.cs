using Firebase.Database;

namespace Firebase_API.Data
{
    public class FirebaseContext
    {
        private readonly FirebaseClient _client;

        public FirebaseContext(string databaseUrl)
        {
            _client = new FirebaseClient(databaseUrl);
        }

        public FirebaseClient Client => _client;
    }
}
