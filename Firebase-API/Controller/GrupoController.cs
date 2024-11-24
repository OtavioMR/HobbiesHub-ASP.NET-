using Microsoft.AspNetCore.Mvc;
using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Firebase_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public GrupoController(IGrupoRepository grupoRepository, IHobbyRepository hobbyRepository, IUsuarioRepository usuarioRepository)
        {
            _grupoRepository = grupoRepository ?? throw new ArgumentNullException(nameof(grupoRepository));
            _hobbyRepository = hobbyRepository ?? throw new ArgumentNullException(nameof(hobbyRepository));
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        [HttpGet]
        public async Task<ActionResult<List<GrupoModel>>> GetGrupos()
        {
            var grupos = await _grupoRepository.GetAllGrupos();
            return Ok(grupos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoModel>> GetGrupo(string id)
        {
            var grupo = await _grupoRepository.GetGrupoById(id);

            if (grupo == null)
            {
                return NotFound();
            }

            return Ok(grupo);
        }

        [HttpPost]
        public async Task<ActionResult<GrupoModel>> CreateGrupo([FromBody] GrupoModel grupo)
        {
            var createdGrupo = await _grupoRepository.AddGrupo(grupo);
            return CreatedAtAction(nameof(GetGrupo), new { id = createdGrupo.Id }, createdGrupo);
        }

        [HttpPost("{id}/adicionar-membro")]
        public async Task<IActionResult> AdicionarMembro(string id, [FromBody] string usuarioId)
        {
            var grupo = await _grupoRepository.GetGrupoById(id);
            if (grupo == null)
            {
                return NotFound();
            }

            var usuario = await _usuarioRepository.GetUsuarioById(usuarioId);
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            grupo.AdicionarMembro(usuarioId);
            await _grupoRepository.UpdateGrupo(grupo, grupo.Id);

            return NoContent();
        }

        [HttpGet("{id}/membros")]
        public async Task<ActionResult<List<UsuarioModel>>> GetMembros(string id)
        {
            var grupo = await _grupoRepository.GetGrupoById(id);

            if (grupo == null)
            {
                return NotFound();
            }

            var membros = new List<UsuarioModel>();
            foreach (var usuarioId in grupo.Membros)
            {
                var usuario = await _usuarioRepository.GetUsuarioById(usuarioId);
                if (usuario != null)
                {
                    membros.Add(usuario);
                }
            }

            return Ok(membros);
        }
    }
}
