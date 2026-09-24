using Mapster;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Models;
using WorkspaceReservas.Services;
using WorkspaceReservas.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WorkspaceReservas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicosServices _medicosService;
        public MedicosController(IMedicosServices medicosService)
        {
            _medicosService = medicosService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicoDTO medico) 
            => ( await _medicosService.Create(medico) )
            .ToActionResult(this, c => CreatedAtAction(nameof(FindById), new { id = c.Id }, c));

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] MedicoUpdateDTO medico)
            => ( await _medicosService.Update(id, medico) ).ToActionResult(this);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
            => ( await _medicosService.Delete(id) ).ToActionResult(this, _ => NoContent());

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
            => ( await _medicosService.FindById(id) ).ToActionResult(this);

        [HttpGet]
        public async Task<IActionResult> List() => ( await _medicosService.FindAll() ).ToActionResult(this);

        [HttpPatch("reativar/{id}")]
        public async Task<IActionResult> Reativar(long id) => ( await _medicosService.Reativar(id) ).ToActionResult(this);
    }
}
