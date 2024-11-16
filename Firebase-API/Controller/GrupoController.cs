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

        /// <summary>
        /// Lista todos os grupos existentes.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<GrupoModel>>> GetGrupos()
        {
            var grupos = await _grupoRepository.GetAllGrupos();
            return Ok(grupos);
        }

        /// <summary>
        /// Obtém um grupo específico pelo ID.
        /// </summary>
        /// <param name="id">ID do grupo</param>
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

        /// <summary>
        /// Cria um novo grupo.
        /// </summary>
        /// <param name="grupo">Dados do grupo a ser criado</param>
        [HttpPost]
        public async Task<ActionResult<GrupoModel>> CreateGrupo([FromBody] GrupoModel grupo)
        {
            // Verifique se o Hobby existe
            var hobby = await _hobbyRepository.GetHobbiesById(grupo.HobbyId);
            if (hobby == null)
            {
                return BadRequest("Hobby não encontrado.");
            }

            // Adicionar o usuário criador como administrador e membro do grupo
            grupo.AdministradorId = grupo.AdministradorId;
            grupo.AdicionarMembro(grupo.AdministradorId);

            var createdGrupo = await _grupoRepository.AddGrupo(grupo);
            return CreatedAtAction(nameof(GetGrupo), new { id = createdGrupo.Id }, createdGrupo);
        }

        /// <summary>
        /// Adiciona um membro ao grupo existente.
        /// </summary>
        /// <param name="id">ID do grupo</param>
        /// <param name="usuarioId">ID do usuário a ser adicionado</param>
        [HttpPost("{id}/adicionar-membro")]
        public async Task<IActionResult> AdicionarMembro(string id, [FromBody] string usuarioId)
        {
            // Verificar se o grupo já existe
            var grupo = await _grupoRepository.GetGrupoById(id);
            if (grupo == null)
            {
                return NotFound();
            }

            // Verifique se o usuário existe
            var usuario = await _usuarioRepository.GetUsuarioById(usuarioId);
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            // Adicionar o usuário ao grupo existente
            grupo.AdicionarMembro(usuarioId);
            await _grupoRepository.UpdateGrupo(grupo, grupo.Id);

            return NoContent();
        }

        /// <summary>
        /// Lista os membros de um grupo com seus dados completos.
        /// </summary>
        /// <param name="id">ID do grupo</param>
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
