using Microsoft.AspNetCore.Mvc;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Services;

namespace WorkspaceReservas.Controllers
{
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
        public IActionResult Create([FromBody] ReservaDTO reserva)
        {
            // Aqui você pode chamar o serviço para criar a reserva
            var createdReserva = _reservaService.Create(reserva);
            if (createdReserva == null) return BadRequest("Não foi possível criar a reserva.");
            return Ok(createdReserva); // Retorna a reserva criada (ou algum outro resultado)
        }

        [HttpPatch]
        public IActionResult Update([FromBody] ReservaDTO reserva)
        {
            var updatedReserva = _reservaService.Update(reserva);
            if (updatedReserva == null) return BadRequest("Não foi possível atualizar a reserva.");
            return Ok(updatedReserva);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedReserva = _reservaService.Delete(id);
            if (deletedReserva == null) return NotFound("Reserva não encontrada.");
            return Ok(deletedReserva);
        }


        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            var reserva = _reservaService.FindById(id);
            if (reserva == null) return NotFound("Reserva não encontrada.");
            return Ok(reserva);
        }

        [HttpGet]
        public IActionResult List()
        {
            var reservas = _reservaService.FindAll();
            return Ok(reservas);
        }
    }
}
