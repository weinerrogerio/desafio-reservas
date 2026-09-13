using Microsoft.AspNetCore.Mvc;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Services;

namespace WorkspaceReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly ISalaServices _salaService;

        public SalaController(ISalaServices salaService)
        {
            _salaService = salaService;
        }

        [HttpPost]
        //[ProducesResponseType(201, Type = typeof(SalaDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(401)]
        //[ProducesResponseType(409)]
        public IActionResult Create([FromBody] SalaDTO sala)
        {
            // Aqui você pode chamar o serviço para criar a sala
            var createdSala = _salaService.Create(sala);
            if ( createdSala == null ) return BadRequest("Não foi possível criar a sala.");          
            return Ok(createdSala); // Retorna a sala criada (ou algum outro resultado)
        }

        [HttpPatch]
        public IActionResult Update([FromBody] SalaDTO sala)
        {
            var updatedSala = _salaService.Update(sala);
            if (updatedSala == null) return BadRequest("Não foi possível atualizar a sala.");
            return Ok(updatedSala);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedSala = _salaService.Delete(id);
            if (deletedSala == null) return NotFound("Sala não encontrada.");
            return Ok(deletedSala);
        }


        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            var sala = _salaService.FindById(id);
            if (sala == null) return NotFound("Sala não encontrada.");
            return Ok(sala);
        }

        [HttpGet]
        public IActionResult List()
        {
            var salas = _salaService.FindAll();
            return Ok(salas);
        }

    }
}
