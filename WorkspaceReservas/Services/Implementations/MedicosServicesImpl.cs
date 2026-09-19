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

            if (string.IsNullOrWhiteSpace(medico.Nome) || string.IsNullOrWhiteSpace(medico.CRM))
            {
                return Result.Fail<MedicoDTO>("O nome e o CRM do médico não podem ser vazios.");
            }

            bool existeConflito = await _context.Medicos.AnyAsync(
                m => m.CRM == medico.CRM &&
                     m.Nome == medico.Nome &&
                     m.Ativo == true);

            if (existeConflito) return Result.Fail<MedicoDTO>("Um medico com esses dados ja cadastrado.");

            var entity = medico.Adapt<Medico>();
            _context.Medicos.Add(entity);
            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }

        public async Task<FluentResults.Result<MedicoUpdateDTO>> Update(MedicoUpdateDTO medico)
        {
            if (medico == null) return Result.Fail<MedicoUpdateDTO>("O objeto médico não pode ser nulo.");
            var entity = await _context.Medicos.FindAsync(medico.Id);
            if (entity == null) return Result.Fail<MedicoUpdateDTO>("Médico não encontrado.");
            // O Mapster copia AUTOMATICAMENTE todas as propriedades do DTO para a Entity mapeada
            //medico.Adapt(entity);
            // validação ja foi feita no controller, mas caso queira validar novamente- fazer antes de salvar no banco de dados (set values)
            _context.Entry(entity).CurrentValues.SetValues(medico);
            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<MedicoUpdateDTO>());
        }

        public async Task<FluentResults.Result<MedicoDTO>> Delete(long id)
        {
            if ( id == 0 || id == null) return Result.Fail<MedicoDTO>("O id nao pode ser nulo.");
            var entity = await _context.Medicos.FindAsync(id);
            if (entity == null) return Result.Fail<MedicoDTO>("Médico não encontrado.");
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
            if (id == 0 || id == null) return Result.Fail<MedicoDTO>("O id nao pode ser nulo.");

            // FirstOrDefaultAsync + AsNoTracking SEMPRE FAZ BUSCA NO BANCO DE DADOS, mesmo que o objeto já esteja no contexto.
            //var entity = await _context.Medicos.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

            // FindAsync FAZ BUSCA NO BANCO DE DADOS SOMENTE SE O OBJETO NÃO ESTIVER NO CONTEXTO, caso contrário, retorna o objeto do contexto.
            var entity = await _context.Medicos.FindAsync(id);
            if ( entity == null) return Result.Fail<MedicoDTO>("Médico não encontrado.");
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }

        public async Task<FluentResults.Result<MedicoDTO>> Reativar(long id)
        {
            if ( id == 0 || id == null) return Result.Fail<MedicoDTO>("O id nao pode ser nulo.");
            //var entity = await _context.Medicos.FindAsync(id);


            var entity = await _context.Medicos
              .IgnoreQueryFilters()
              .FirstOrDefaultAsync(s => s.Id == id);


            if ( entity == null) return Result.Fail<MedicoDTO>("Médico não encontrado.");
            entity.Ativo = true;
            _context.Medicos.Update(entity);
            await _context.SaveChangesAsync();
            return Result.Ok(entity.Adapt<MedicoDTO>());
        }

        
    }
}
