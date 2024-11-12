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
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> CreateUsuario(UsuarioModel usuario)
        {
            // Garantir que a data de nascimento tenha apenas a parte da data
            if (usuario.DateOfBirth != null)
            {
                usuario.DateOfBirth = usuario.DateOfBirth.Date; // Remove a hora
            }

            // Adiciona o usuário ao Firebase, sem a necessidade de fornecer o 'Id'
            var createdUsuario = await _usuarioRepository.AddUsuario(usuario);

            // A API retorna o 'Id' gerado automaticamente pelo Firebase
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
    }
}
