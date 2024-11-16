using Firebase_API.Models;
using Firebase_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firebase_API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly IHobbyRepository _hobbyRepository;
        public HobbyController(IHobbyRepository hobbyRepository)
        {
            _hobbyRepository = hobbyRepository;
        }

        // Método para listar todos os hobbies
        [HttpGet]
        public async Task<ActionResult<List<HobbyModel>>> GetHobbies()
        {
            var hobbies = await _hobbyRepository.GetAllHobbies();
            return Ok(hobbies);
        }


        // Método para obter um hobby específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<HobbyModel>> GetHobby(string id)
        {
            var hobby = await _hobbyRepository.GetHobbiesById(id);

            if (hobby == null)
            {
                return NotFound();
            }

            return Ok(hobby);
        }


        // Método para criar um novo hobby
        [HttpPost]
        public async Task<ActionResult<HobbyModel>> CreateHobby(HobbyModel hobby)
        {
            // Adiciona o hobby ao Firebase, sem a necessidade de fornecer o 'Id'
            var createHobby = await _hobbyRepository.AddHobby(hobby);

            // A API retorna o 'Id' gerado automaticamente pelo Firebase
            return CreatedAtAction(nameof(GetHobby), new { id = createHobby.Id }, createHobby);
        }


        // Método para atualizar um hobby existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHobby(string id, HobbyModel hobby)
        {
            var existingHobby = await _hobbyRepository.GetHobbiesById(id);

            if (existingHobby == null)
            {
                return NotFound();
            }

            hobby.Id = id;
            await _hobbyRepository.UpdateHobby(hobby, id);

            return NoContent();
        }


        // Método para deletar um hobby
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(string id)
        {
            var existingHobby = await _hobbyRepository.GetHobbiesById(id);

            if ( existingHobby == null)
            {
                return NotFound();
            }

            await _hobbyRepository.DeleteHobby(id);

            return NoContent();
        }
    }
}
