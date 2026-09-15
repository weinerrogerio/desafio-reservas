using FluentAssertions.Execution;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;
using System.Collections.Generic; // adicionado se necessário
using System.ComponentModel;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Context;
using WorkspaceReservas.Utils;

namespace WorkspaceReservas.Services.Implementations
{
    public class SalaServicesImpl : ISalaServices
    {
        private readonly PostgreSQLContext _context;
        //private readonly NumberHelper _numberHelper;

        public SalaServicesImpl(PostgreSQLContext context)
        {
            _context = context;
        }        

        public async Task<SalaDTO> Create(SalaDTO sala)
        {      
            var entity = sala.Adapt<Sala>();
            _context.Salas.Add(entity);
            await _context.SaveChangesAsync();
            return await Task.FromResult(entity.Adapt<SalaDTO>());
        }


        public async Task<SalaDTO> Update(SalaDTO sala)
        {
            if (sala == null) return null;
            var existing = await _context.Salas.FindAsync(sala.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(sala);
            _context.SaveChanges();
            return sala;
        }

        public async Task<SalaDTO> Delete(long id)
        {
            var existing = await _context.Salas.FindAsync(id);
            if (existing == null) return null;
            _context.Salas.Remove(existing);
            _context.SaveChanges();
            return existing.Adapt<SalaDTO>();
        }

        public async Task<SalaDTO> Reativar(long id)
        {
            var existing = await _context.Salas
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(s => s.Id == id);

            if ( existing == null ) return null;
            existing.Ativo = true;
            await _context.SaveChangesAsync();
            return existing.Adapt<SalaDTO>();
        }

        public async Task<SalaDTO> FindById(long id)
        {
            var entity = await _context.Salas.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id && s.Ativo);
            return entity?.Adapt<SalaDTO>();
        }     

        public async Task<List<SalaDTO>> FindAll()
        {
            var entities = await _context.Salas.AsNoTracking().ToListAsync();
            return entities.Where(s => s.Ativo).Adapt<List<SalaDTO>>();
        }

    }
}
