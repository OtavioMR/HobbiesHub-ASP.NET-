using Microsoft.AspNetCore.Mvc;
using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firebase_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Método para listar todos os usuários
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllUsuarios();
            return Ok(usuarios);
        }

        // Método para obter um usuário específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuario(string id)
        {
            var usuario = await _usuarioRepository.GetUsuarioById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // Método para criar um novo usuário
        [HttpPost("register")]
        public async Task<ActionResult<UsuarioModel>> Register([FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Usuário não pode ser vazio.");
            }

            // Validação do modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUsuario = await _usuarioRepository.AddUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.Id }, createdUsuario);
        }

        // Método para atualizar um usuário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(string id, UsuarioModel usuario)
        {
            var existingUsuario = await _usuarioRepository.GetUsuarioById(id);

            if (existingUsuario == null)
            {
                return NotFound();
            }

            usuario.Id = id;
            await _usuarioRepository.UpdateUsuario(usuario, id);

            return NoContent();
        }

        // Método para deletar um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            var existingUsuario = await _usuarioRepository.GetUsuarioById(id);

            if (existingUsuario == null)
            {
                return NotFound();
            }

            await _usuarioRepository.DeleteUsuario(id);

            return NoContent();
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<UsuarioModel>> Authenticate(string email, string password)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAndPassword(email, password);

            if (usuario == null)
            {
                return Unauthorized("Email ou senha incorretos.");
            }

            return Ok(usuario);
        }
    }
}
