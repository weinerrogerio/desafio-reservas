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
        {
            var result = await _consultaService.Create(consulta);
            if ( result.IsFailed )
            {
                // Retorna HTTP 400 Bad Request contendo a lista de erros do FluentResults
                return BadRequest(result.Errors.Select(e => e.Message));
            }
            return Ok(result.Value);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ConsultaUpdateDTO consulta)
        {
            var result = await _consultaService.Update(id, consulta);
            if ( result.IsFailed )
            {
                // Retorna HTTP 400 Bad Request contendo a lista de erros do FluentResults
                return BadRequest(result.Errors.Select(e => e.Message));
            }
            return Ok(result.Value);
        }

        // FORMA COMENTADA DELETA, POIS NÃO ESTÁ TRATANDO OS ERROS DE FORMA ADEQUADA
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(long id)
        //{
        //    var result = await _consultaService.Delete(id);
        //    if ( result.IsFailed )
        //    {
        //        return BadRequest(result.Errors.Select(e => e.Message));
        //    }
        //    return Ok(result.Value);
        //}

        // NOVA FORMA DELETA, TRATANDO OS ERROS DE FORMA ADEQUADA
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(long id)
        //{
        //    var result = await _consultaService.Delete(id);

        //    if ( result.IsSuccess )
        //        return NoContent(); // HTTP 204 No Content para exclusões com sucesso

        //    // Mapeamento correto dos status HTTP com base no tipo de erro
        //    var firstError = result.Errors.FirstOrDefault()?.Message ?? "Erro desconhecido.";

        //    if ( firstError.Contains("não encontrada") )
        //        return NotFound(new { error = firstError }); // HTTP 404

        //    if ( firstError.Contains("menos de 2 horas") )
        //        return UnprocessableEntity(new { error = firstError }); // HTTP 422 (Regra de Negócio)

        //    return BadRequest(new { error = firstError }); // HTTP 400
        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
             => ( await _consultaService.Delete(id) ).ToActionResult(this, _ => NoContent());

    }
}
