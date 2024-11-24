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
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuario(string id)
        {
            UsuarioModel? usuario = await _usuarioRepository.GetUsuarioById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioModel>> CreateUsuario(UsuarioModel usuario)
        {
            usuario.DateOfBirth = usuario.DateOfBirth.Date;

            var createdUsuario = await _usuarioRepository.AddUsuario(usuario);

            return CreatedAtAction(nameof(GetUsuario), new { id = createdUsuario.Id }, createdUsuario);
        }
    }
}
