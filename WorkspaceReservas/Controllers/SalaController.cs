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
            var createdSala = _salaService.create(sala);
            if ( createdSala == null ) return BadRequest("Não foi possível criar a sala.");          
            return Ok(createdSala); // Retorna a sala criada (ou algum outro resultado)
        }
    }
}
