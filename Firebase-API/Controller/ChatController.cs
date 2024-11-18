using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;
using Firebase.Database;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  // Adicione esta linha para melhorar a vinculação do modelo
    public class ChatController : ControllerBase  // Use ControllerBase se não precisar de funcionalidades de View
    {
        private readonly FirebaseService _firebaseService;

        public ChatController(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage chatMessage)  // Adicionando [FromBody] para vincular a partir do corpo JSON
        {
            await _firebaseService.GetClient()
                .Child("messages")
                .PostAsync(chatMessage);

            return Ok();
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _firebaseService.GetClient()
                .Child("messages")
                .OnceAsync<ChatMessage>();

            var sortedMessages = messages
                .OrderBy(m => m.Object.Timestamp)
                .Select(m => m.Object);

            return Ok(sortedMessages);
        }
    }
}
