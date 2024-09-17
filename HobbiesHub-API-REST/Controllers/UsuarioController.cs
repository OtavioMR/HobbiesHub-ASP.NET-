using HobbiesHub_API_REST.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HobbiesHub_API_REST.Controllers
{
    // Definindo a rota do controlador
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        // Construtor correto com a interface IUsuarioRepository
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Método para criar um usuário
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> AddUsuario([FromBody] UsuarioModel usuario)
        {
            try
            {
                UsuarioModel novoUsuario = await _usuarioRepository.AddUsuario(usuario);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = novoUsuario.Id }, novoUsuario); // Retorna o novo usuário criado
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao adicionar usuário: {ex.Message}" });
            }
        }

        // Método para pegar todos os usuários
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> GetAllUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepository.GetAllUsuarios();
            return Ok(usuarios);
        }

        // Método para pegar o usuário por id
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioById(int id)
        {
            UsuarioModel usuario = await _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            return Ok(usuario);
        }

        // Método para atualizar o usuário
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> UpdateUsuario(int id, [FromBody] UsuarioModel usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest(new { message = "ID do usuário não corresponde ao ID no corpo da solicitação." });
            }

            try
            {
                UsuarioModel usuarioAtualizado = await _usuarioRepository.UpdateUsuario(usuario);
                if (usuarioAtualizado == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }
                return Ok(usuarioAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao atualizar usuário: {ex.Message}" });
            }
        }

        // Método para deletar o usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            bool deleted = await _usuarioRepository.DeleteUsuario(id);
            if (!deleted)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }
            return NoContent(); // Retorna 204 No Content se a exclusão for bem-sucedida
        }
    }
}
