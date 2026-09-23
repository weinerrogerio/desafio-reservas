using Mapster;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Models;
using WorkspaceReservas.Services;
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
        {
            var result = await _medicosService.Create(medico);

            if ( result.IsFailed )
            {
                // Retorna HTTP 400 Bad Request contendo a lista de erros do FluentResults
                return BadRequest(result.Errors.Select(e => e.Message));
            }

            return Ok(result.Value);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] MedicoUpdateDTO medico)
        {
            var result = await _medicosService.Update(id, medico);
            if (result.IsFailed)
                return BadRequest(result.Errors.Select(e => e.Message));
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _medicosService.Delete(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(e => e.Message));
            }
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(long id)
        {
            var result = await _medicosService.FindById(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(e => e.Message));
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _medicosService.FindAll();
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(e => e.Message));
            }
            return Ok(result.Value);
        }

        [HttpPatch("reativar/{id}")]
        public async Task<IActionResult> Reativar(long id)
        {
            var result = await _medicosService.Reativar(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.Select(e => e.Message));
            }
            return Ok(result.Value);
        }
    }
}
