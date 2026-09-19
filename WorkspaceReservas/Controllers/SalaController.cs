using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
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
        public async Task<IActionResult> Create([FromBody] SalaDTO request)
        {
            // Se chegou aqui, o ASP.NET já executou o FluentValidation e garantiu que é válido!
            var novoId = await _salaService.Create(request);

            return Ok(new { Id = novoId });
        }

        //[ProducesResponseType(201, Type = typeof(SalaDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(401)]
        //[ProducesResponseType(409)]
        //public IActionResult Create([FromBody] SalaDTO sala)
        //{
        //    // Aqui você pode chamar o serviço para criar a sala

        //    var createdSala = _salaService.Create(sala);
        //    if ( createdSala == null ) return BadRequest("Não foi possível criar a sala.");          
        //    return Ok(createdSala); // Retorna a sala criada (ou algum outro resultado)
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody] SalaDTO sala, [FromServices] IValidator<SalaDTO> validator)
        //{
        //    var result = validator.Validate(sala);
        //    if (!result.IsValid)
        //    {
        //        foreach (var error in result.Errors)
        //            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //        return ValidationProblem(ModelState);
        //    }

        //var result = _salaService.Create(sala);
        //if ( !result.IsSuccess )
        //{
        //    foreach ( var erro in result.Errors )
        //        foreach ( var mensagem in erro.Value )
        //            ModelState.AddModelError(erro.Key, mensagem);

        //    return ValidationProblem(ModelState);
        //}

        //return Ok(result.Value);

        //}
                
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] SalaUpdateDTO sala)
        {
            var existente = await _salaService.FindById(sala.Id);
            if (existente == null) return NotFound("Sala não encontrada.");

            var toUpdate = new SalaDTO
            {
                Id = existente.Id,
                Nome = sala.Nome ?? existente.Nome,
                Capacidade = sala.Capacidade ?? existente.Capacidade,
                PrecoPorHora = sala.PrecoPorHora ?? existente.PrecoPorHora,
                Ativo = sala.Ativo ?? existente.Ativo
            };

            var updatedSala = await _salaService.Update(toUpdate);
            if (updatedSala == null) return BadRequest("Não foi possível atualizar a sala.");
            return Ok(updatedSala);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedSala = await _salaService.Delete(id);
            if (deletedSala == null) return NotFound("Sala não encontrada.");
            return Ok(deletedSala);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var sala = await _salaService.FindById(id);
            if (sala == null) return NotFound("Sala não encontrada.");
            return Ok(sala);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var salas = await _salaService.FindAll();
            return Ok(salas);
        }

        [HttpPut("{id}/reativar")]
        public async Task<IActionResult> Reativar(long id)
        {
            var sala = await _salaService.Reativar(id);
            if (sala == null) return NotFound("Sala não encontrada.");
            return Ok(sala);
        }

    }
}
