using HobbiesHub_API_REST.Models;
using HobbiesHub_API_REST.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HobbiesHub_API_REST.Repositories;
using System.Text.RegularExpressions;

namespace HobbiesHub_API_REST.Controllers
{
    // Definindo a rota do controlador
    [Route("api/[controller]")]
    [ApiController]

    public class GrupoController : ControllerBase
    {
        private readonly IGrupoRepository _grupoRepository;

        // Construtor correto com a interface IGrupoRepository
        public GrupoController(IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        // Método para criar um grupo
        [HttpPost]
        public async Task<ActionResult<GrupoModel>> AddGrupo([FromBody] GrupoModel grupo)
        {
            try
            {
                GrupoModel novoGrupo = await _grupoRepository.AddGrupo(grupo);
                return CreatedAtAction(nameof(GetGrupoById), new { id = novoGrupo.Id }, novoGrupo); // Retorna o novo usuário criado
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao adicionar grupo: {ex.Message}" });
            }
        }

        // Método para pegar todos os grupo
        [HttpGet]
        public async Task<ActionResult<List<GrupoModel>>> GetAllGrupos()
        {
            List<GrupoModel> grupos = await _grupoRepository.GetAllGrupos();
            return Ok(grupos);
        }

        // Método para pegar o grupo por id
        [HttpGet("{id}")]
        public async Task<ActionResult<GrupoModel>> GetGrupoById(int id)
        {
            GrupoModel grupo = await _grupoRepository.GetGrupoById(id);
            if (grupo == null)
            {
                return NotFound(new { message = "Grupo não encontrado." });
            }
            return Ok(grupo);
        }

        // Método para atualizar o grupo
        [HttpPut("{id}")]
        public async Task<ActionResult<GrupoModel>> UpdateGrupo(int id, [FromBody] GrupoModel grupo)
        {
            if (id != grupo.Id)
            {
                return BadRequest(new { message = "ID do grupo não corresponde ao ID no corpo da solicitação." });
            }

            try
            {
                GrupoModel grupoAtualizado = await _grupoRepository.UpdateGrupo(grupo, id); // Passa o id também
                if (grupoAtualizado == null)
                {
                    return NotFound(new { message = "Grupo não encontrado." });
                }
                return Ok(grupoAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao atualizar grupo: {ex.Message}" });
            }
        }

        // Método para deletar o grupo
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupo(int id)
        {
            try
            {
                bool deleted = await _grupoRepository.DeleteGrupo(id);

                if (!deleted) // Verifica se a exclusão foi bem-sucedida
                {
                    return NotFound(new { message = "Grupo não encontrado." });
                }

                return NoContent(); // Se o grupo foi excluído com sucesso, retorna 204 No Content
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao deletar o grupo: {ex.Message}" });
            }
        }

    }
}
