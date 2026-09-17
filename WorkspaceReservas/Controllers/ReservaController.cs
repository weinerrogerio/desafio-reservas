using Microsoft.AspNetCore.Mvc;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Services;

namespace WorkspaceReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaServices _reservaService;

        public ReservaController(IReservaServices reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        //[ProducesResponseType(201, Type = typeof(SalaDTO))]
        //[ProducesResponseType(400)]   
        //[ProducesResponseType(401)]
        //[ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] ReservaDTO reserva)
        {
            var result = await _reservaService.Create(reserva);
            if ( !result.Success )
            {
                return BadRequest(result.Errors); // Retorna HTTP 400 com os erros detalhados
            }
            return CreatedAtAction(nameof(FindById), new { id = result.Data!.Id }, result.Data); // Retorna HTTP 201 Created
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] ReservaDTO reserva)
        {
            //var updatedReserva = await _reservaService.Update(reserva);
            //if (updatedReserva == null) return BadRequest("Não foi possível atualizar a reserva.");
            //return Ok(updatedReserva);

            var result = await _reservaService.Update(reserva);
            if (!result.Success)
            {
                return BadRequest(result.Errors); // Retorna HTTP 400 com os erros detalhados
            }
            return Ok(result.Data);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedReserva = await _reservaService.Delete(id);
            if (deletedReserva == null) return NotFound("Reserva não encontrada.");
            return Ok(deletedReserva);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var reserva = await _reservaService.FindById(id);
            if (reserva == null) return NotFound("Reserva não encontrada.");
            return Ok(reserva);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var reservas = await _reservaService.FindAll();
            return Ok(reservas);
        }
    }
}
