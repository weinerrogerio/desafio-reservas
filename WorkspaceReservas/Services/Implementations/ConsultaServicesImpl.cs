using FluentResults;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Context;
using WorkspaceReservas.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkspaceReservas.Services.Implementations
{
    public class ConsultaServicesImpl : IConsultaServices
    {


        private readonly PostgreSQLContext _context;

        public ConsultaServicesImpl(PostgreSQLContext context)
        {
            _context = context;
        }


        public async Task<FluentResults.Result<ConsultaDTO>> Create(ConsultaDTO consulta)
        {
            var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == consulta.MedicoIdFk);
            if ( !medicoExiste )
                return Result.Fail<ConsultaDTO>(new NotFoundError($"Médico com ID {consulta.MedicoIdFk} não encontrado."));

            bool existeConflito = await _context.Consultas
                .AnyAsync(c => c.MedicoIdFk == consulta.MedicoIdFk &&
                               consulta.DataHoraInicio < c.DataHoraFim &&
                               consulta.DataHoraFim > c.DataHoraInicio);

            if ( existeConflito ) return Result.Fail<ConsultaDTO>(new ConflictError("Já existe uma consulta nesse horário para esse médico.")); ;

            var entity = consulta.Adapt<Consulta>();
            _context.Consultas.Add(entity);
            await _context.SaveChangesAsync();

            return Result.Ok(entity.Adapt<ConsultaDTO>());

        }

        public async Task<FluentResults.Result<ConsultaDTO>> Update(long id, ConsultaUpdateDTO consulta)
        {
            var entity = await _context.Consultas.FindAsync(id);
            if ( entity == null )
                return Result.Fail<ConsultaDTO>(new NotFoundError("Consulta não encontrada."));

            // 1. Validar se o médico existe (apenas se fornecido)
            if ( consulta.MedicoIdFk.HasValue )
            {
                var medicoExiste = await _context.Medicos.AnyAsync(m => m.Id == consulta.MedicoIdFk.Value);
                if ( !medicoExiste )
                    return Result.Fail<ConsultaDTO>(new NotFoundError($"Médico com ID {consulta.MedicoIdFk.Value} não encontrado."));
            }

            // 2. Determina os valores finais do registro
            // Se o campo do DTO for nulo, usa o valor atual já gravado na 'entity'
            var medicoIdFinal = consulta.MedicoIdFk ?? entity.MedicoIdFk;
            var inicioFinal = consulta.DataHoraInicio ?? entity.DataHoraInicio;
            var fimFinal = consulta.DataHoraFim ?? entity.DataHoraFim;

            // 3. Validação de consistência do horário final vs inicial
            if ( fimFinal <= inicioFinal )
            {
                return Result.Fail<ConsultaDTO>(new BusinessRuleError("A data/hora de fim deve ser maior que a data/hora de início."));
            }

            // 4. Checa conflito no banco de dados usando os valores FINAIS
            // IMPORTANTE: 'c.Id != id' é necessário para não comparar a consulta com ela mesma!
            bool existeConflito = await _context.Consultas
                .AnyAsync(c => c.Id != id &&
                               c.MedicoIdFk == medicoIdFinal &&
                               inicioFinal < c.DataHoraFim &&
                               fimFinal > c.DataHoraInicio);

            if ( existeConflito )
                return Result.Fail<ConsultaDTO>(new ConflictError("Não é possível atualizar. Já existe uma consulta nesse horário para esse médico."));

            // 5. Mapeia da DTO para a entidade existente IGNORANDO campos nulos
            consulta.Adapt(entity);

            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<ConsultaDTO>());
        }

        public async Task<FluentResults.Result<ConsultaDTO>> Delete(long id)
        {
            var entity = await _context.Consultas.FindAsync(id);
            if ( entity == null )
                return Result.Fail<ConsultaDTO>(new NotFoundError("Consulta não encontrada, não pode ser deletada."));

            // TODO: ARRUMAR ISSO, NÃO PERMITE EXCCLUIR DADOS DO DIA ANTERIOR - rever regra de negócio
            Log.Warning($"Tentativa de exclusão de consulta com menos de 2 horas para o início.... {DateTime.Now} - {entity.DataHoraInicio} - {DateTime.Now.AddHours(2)}");

            if ( entity.DataHoraInicio <= DateTime.Now.AddHours(2) )
                return Result.Fail<ConsultaDTO>(
                    new BusinessRuleError(
                        "A consulta não pode ser deletada se restam menos de 2 horas para o início."
                        ));

            _context.Consultas.Remove(entity);
            await _context.SaveChangesAsync();

            return Result.Ok(entity.Adapt<ConsultaDTO>());
        }

        public async Task<FluentResults.Result<List<ConsultaDTO>>> FindAll()
        {
            var entities = await _context.Consultas.ToListAsync();
            return Result.Ok(entities.Select(e => e.Adapt<ConsultaDTO>()).ToList());
        }

        public async Task<FluentResults.Result<ConsultaDTO>> FindById(long id)
        {
            var entity = await _context.Consultas.FindAsync(id);
            if ( entity == null ) return Result.Fail<ConsultaDTO>(new NotFoundError("Consulta não encontrada."));
            return Result.Ok(entity.Adapt<ConsultaDTO>());
        }        

    }
}
