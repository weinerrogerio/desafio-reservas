using FluentResults;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Context;
using WorkspaceReservas.Utils;

namespace WorkspaceReservas.Services.Implementations
{
    public class MedicosServicesImpl : IMedicosServices
    {
        private readonly PostgreSQLContext _context;

        public MedicosServicesImpl(PostgreSQLContext context)
        {
            _context = context;
        }
        public async Task<FluentResults.Result<MedicoDTO>> Create(MedicoDTO medico)
        {
            if (medico == null) return Result.Fail<MedicoDTO>("O objeto médico não pode ser nulo.");

            bool existeConflito = await _context.Medicos.AnyAsync(
                m => m.CRM == medico.CRM &&
                     m.Ativo == true);

            if (existeConflito) return Result.Fail<MedicoDTO>(new ConflictError("Um medico com esses dados ja cadastrado."));

            var entity = medico.Adapt<Medico>();
            _context.Medicos.Add(entity);
            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }        

        public async Task<FluentResults.Result<MedicoDTO>> Update(long id, MedicoUpdateDTO dto)
        {
            var entity = await _context.Medicos.FindAsync(id);
            if (entity == null) return Result.Fail<MedicoDTO>(new NotFoundError("Médico não encontrado."));

            dto.Adapt(entity); // copia só os campos não nulos para a entidade existente

            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }

        public async Task<FluentResults.Result<MedicoDTO>> Delete(long id)
        {
            var entity = await _context.Medicos.FindAsync(id);
            if (entity == null) return Result.Fail<MedicoDTO>(new NotFoundError("Médico não encontrado."));
            _context.Medicos.Remove(entity);
            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }

        public async Task<FluentResults.Result<List<MedicoDTO>>> FindAll()
        {
            //var entities = await _context.Medicos.AsNoTracking().FirstOrDefaultAsync(m => m.Ativo == true);
            var entities = await _context.Medicos.AsNoTracking().Where(m => m.Ativo == true).ToListAsync();
            return Result.Ok(entities.Adapt<List<MedicoDTO>>());
        }

        public async Task<FluentResults.Result<MedicoDTO>> FindById(long id)
        {
            // FindAsync FAZ BUSCA NO BANCO DE DADOS SOMENTE SE O OBJETO NÃO ESTIVER NO CONTEXTO, caso contrário, retorna o objeto do contexto.
            var entity = await _context.Medicos.FindAsync(id);
            if ( entity == null) return Result.Fail<MedicoDTO>(new NotFoundError("Médico não encontrado."));
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }

        public async Task<FluentResults.Result<MedicoDTO>> Reativar(long id)
        {
            var entity = await _context.Medicos
              .IgnoreQueryFilters()
              .FirstOrDefaultAsync(s => s.Id == id);

            if ( entity == null) return Result.Fail<MedicoDTO>(new NotFoundError("Médico não encontrado."));
            entity.Ativo = true;
            _context.Medicos.Update(entity);
            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }

        
    }
}
