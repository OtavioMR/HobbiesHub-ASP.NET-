using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly FirebaseClient _firebaseClient;

    public ChatController(FirebaseClient firebaseClient)
    {
        _firebaseClient = firebaseClient;
    }

    // Endpoint para enviar uma mensagem (POST)
    [HttpPost("sendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
    {
        if (message == null)
        {
            return BadRequest("Mensagem não pode ser vazia.");
        }

        // Serializando a mensagem para o Firebase (convertendo para um formato compatível)
        var result = await _firebaseClient
            .Child("messages") // Acessa a coleção "messages"
            .PostAsync(message); // Envia o objeto message serializado para o Firebase

        return Ok(result);
    }


    // Novo endpoint GET para buscar mensagens de um grupo
    [HttpGet("getMessages")]
    public async Task<IActionResult> GetMessages([FromQuery] string groupId)
    {
        if (string.IsNullOrEmpty(groupId))
        {
            return BadRequest("O ID do grupo é obrigatório.");
        }

        try
        {
            // Recupera todas as mensagens da coleção "messages"
            var allMessages = await _firebaseClient
                .Child("messages")
                .OnceAsync<ChatMessage>();

            // Filtra mensagens para o grupo especificado
            var groupMessages = allMessages
                .Where(m => m.Object.GroupId == groupId) // Certifique-se de que "GroupId" existe no ChatMessage
                .Select(m => m.Object)
                .ToList();

            if (groupMessages == null || !groupMessages.Any())
            {
                return NotFound("Nenhuma mensagem encontrada para o grupo especificado.");
            }

            return Ok(groupMessages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao recuperar mensagens: {ex.Message}");
        }
    }


}
