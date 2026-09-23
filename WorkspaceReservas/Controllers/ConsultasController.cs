using Microsoft.AspNetCore.Mvc;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Services;
using WorkspaceReservas.Utils;

namespace WorkspaceReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaServices _consultaService;
        public ConsultasController(IConsultaServices consultaService)
        {
            _consultaService = consultaService;
        }               

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ConsultaDTO consulta)
             => ( await _consultaService.Create(consulta) )
            .ToActionResult(this, c => CreatedAtAction(nameof(FindById), new { id = c.Id }, c));

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ConsultaUpdateDTO consulta)
             => ( await _consultaService.Update(id, consulta) ).ToActionResult(this);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
             => ( await _consultaService.Delete(id) ).ToActionResult(this, _ => NoContent());

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
             => ( await _consultaService.FindById(id) ).ToActionResult(this);

        [HttpGet]
        public async Task<IActionResult> List()
             => ( await _consultaService.FindAll() ).ToActionResult(this);

    }
}
