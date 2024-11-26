using Microsoft.AspNetCore.Mvc;
using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firebase_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoController(IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;
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
        public async Task<ActionResult<GrupoModel>> CreateGrupo(GrupoModel grupo)
        {
            var createdGrupo = await _grupoRepository.AddGrupo(grupo);
            return CreatedAtAction(nameof(GetGrupo), new { id = createdGrupo.Id }, createdGrupo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupo(string id, GrupoModel grupo)
        {
            var existingGrupo = await _grupoRepository.GetGrupoById(id);

            if (existingGrupo == null)
            {
                return NotFound();
            }

            grupo.Id = id;
            await _grupoRepository.UpdateGrupo(grupo, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupo(string id)
        {
            var existingGrupo = await _grupoRepository.GetGrupoById(id);

            if (existingGrupo == null)
            {
                return NotFound();
            }

            await _grupoRepository.DeleteGrupo(id);

            return NoContent();
        }
    }
}
