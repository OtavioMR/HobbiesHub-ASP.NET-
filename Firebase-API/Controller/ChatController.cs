using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Firebase_API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Firebase_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly FirebaseClient _firebaseClient;

        public ChatController(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageModel message)
        {
            if (message == null || string.IsNullOrEmpty(message.GroupId) || string.IsNullOrEmpty(message.UserId) || string.IsNullOrEmpty(message.Texto))
            {
                return BadRequest(new { message = "Todos os campos são obrigatórios." });
            }

            message.Hora = DateTime.UtcNow;

            var result = await _firebaseClient
                .Child("groups")
                .Child(message.GroupId)
                .Child("mensagens")
                .PostAsync(message);

            return Ok(result);
        }

        [HttpGet("getMessages")]
        public async Task<IActionResult> GetMessages([FromQuery] string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                return BadRequest(new { message = "O ID do grupo é obrigatório." });
            }

            try
            {
                var groupMessages = await _firebaseClient
                    .Child("groups")
                    .Child(groupId)
                    .Child("mensagens")
                    .OnceAsync<ChatMessageModel>();

                var messages = groupMessages
                    .Select(m => new ChatMessageModel
                    {
                        Id = m.Object.Id,
                        GroupId = m.Object.GroupId,
                        UserId = m.Object.UserId,
                        Texto = m.Object.Texto,
                        Hora = m.Object.Hora
                    })
                    .ToList();

                if (!messages.Any())
                {
                    return NotFound(new { message = "Nenhuma mensagem encontrada para o grupo especificado." });
                }

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao recuperar mensagens: {ex.Message}");
            }
        }
    }
}
