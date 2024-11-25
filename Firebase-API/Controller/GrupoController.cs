using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        // Método para obter um hobby pelo ID
        [HttpGet("hobby/{id}")]
        public async Task<IActionResult> GetHobbyById(string id)
        {
            var hobby = await _hobbyRepository.GetHobbiesById(id);
            if (hobby == null)
            {
                return NotFound($"Hobby com ID {id} não encontrado.");
            }

            return Ok(hobby);
        }

        // Método para obter todos os grupos
        [HttpGet]
        public async Task<ActionResult<List<GrupoModel>>> GetGrupos()
        {
            var grupos = await _grupoRepository.GetAllGrupos();
            return Ok(grupos);
        }

        // Método para obter um grupo específico pelo ID
        [HttpGet("grupo/{id}")]
        public async Task<ActionResult<GrupoModel>> GetGrupo(string id)
        {
            var grupo = await _grupoRepository.GetGrupoById(id);

            if (grupo == null)
            {
                return NotFound();
            }

            return Ok(grupo);
        }

        // Método para criar um grupo
        [HttpPost]
        public async Task<ActionResult<GrupoModel>> CreateGrupo([FromBody] GrupoModel grupo)
        {
            if (grupo == null)
            {
                return BadRequest("O grupo não pode ser nulo.");
            }

            // Validação para garantir que o hobby está associado
            var hobby = await _hobbyRepository.GetHobbiesById(grupo.HobbyId);
            if (hobby == null)
            {
                return BadRequest("Hobby não encontrado.");
            }

            // Adicionar o usuário atual como administrador do grupo
            var usuarioId = User.Identity.Name; // Pegar o ID do usuário logado
            grupo.AdministradorId = usuarioId;
            grupo.AdicionarMembro(usuarioId); // Adicionar o administrador à lista de membros

            var createdGrupo = await _grupoRepository.AddGrupo(grupo);
            return CreatedAtAction(nameof(GetGrupo), new { id = createdGrupo.Id }, createdGrupo);
        }

        // Método para adicionar um membro ao grupo
        [HttpPost("{id}/adicionar-membro")]
        public async Task<IActionResult> AdicionarMembro(string id, [FromBody] string usuarioId)
        {
            if (string.IsNullOrEmpty(usuarioId))
            {
                return BadRequest("ID de usuário não pode ser nulo ou vazio.");
            }

            var grupo = await _grupoRepository.GetGrupoById(id);
            if (grupo == null)
            {
                return NotFound("Grupo não encontrado.");
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

        // Método para obter os membros de um grupo
        [HttpGet("grupo/{id}/membros")]
        public async Task<ActionResult<List<UsuarioModel>>> GetMembros(string id)
        {
            var grupo = await _grupoRepository.GetGrupoById(id);
            if (grupo == null)
            {
                return NotFound("Grupo não encontrado.");
            }

            // Agora, buscamos todos os membros usando seus IDs
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
