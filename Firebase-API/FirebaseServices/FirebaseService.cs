using Firebase.Database;

namespace YourNamespace.Services
{
    public class FirebaseService
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService(string basePath)
        {
            _firebaseClient = new FirebaseClient(basePath);
        }

        public FirebaseClient GetClient()
        {
            return _firebaseClient;
        }
    }
}
