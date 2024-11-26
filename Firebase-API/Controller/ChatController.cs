using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Firebase_API.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
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
        if (message == null)
        {
            return BadRequest("Mensagem não pode ser vazia.");
        }

        message.Hora = DateTime.UtcNow; // Definir a hora de envio da mensagem

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
            return BadRequest("O ID do grupo é obrigatório.");
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
                    Id = m.Key,
                    GroupId = m.Object.GroupId,
                    UserId = m.Object.UserId,
                    Texto = m.Object.Texto,
                    Hora = m.Object.Hora
                })
                .ToList();

            if (messages == null || !messages.Any())
            {
                return NotFound("Nenhuma mensagem encontrada para o grupo especificado.");
            }

            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao recuperar mensagens: {ex.Message}");
        }
    }
}
