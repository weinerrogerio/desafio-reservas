using FluentAssertions.Execution;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;
using System.ComponentModel;
using System.Collections.Generic; // adicionado se necessário
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Context;

namespace WorkspaceReservas.Services.Implementations
{
    public class SalaServicesImpl : ISalaServices
    {
        private readonly PostgreSQLContext _context;       

        public SalaServicesImpl(PostgreSQLContext context)
        {
            _context = context;
        }

        // Renomeado para corresponder à interface (Create)
        public SalaDTO Create(SalaDTO sala)
        {
            if (sala == null) return null;
            var entity = sala.Adapt<Sala>();
            _context.Salas.Add(entity);
            _context.SaveChanges();
            return entity.Adapt<SalaDTO>();            
        }

        // Renomeado e implementado para corresponder à interface (Update)
        public SalaDTO Update(SalaDTO sala)
        {
            if (sala == null) return null;
            var existing = _context.Salas.Find(sala.Id);
            if (existing == null) return null;
            // Atualiza propriedades (Mapster pode mapear para a entidade existente)
            //sala.Adapt(existing);
            //_context.Salas.Update(existing);
            //_context.SaveChanges();
            //return existing.Adapt<SalaDTO>();
            _context.Entry(existing).CurrentValues.SetValues(sala);
            _context.SaveChanges();
            return sala;
        }

        // Renomeado e implementado para corresponder à interface (Delete)
        public SalaDTO Delete(long id)
        {
            var existing = _context.Salas.Find(id);
            if (existing == null) return null;
            _context.Salas.Remove(existing);
            _context.SaveChanges();
            return existing.Adapt<SalaDTO>();
        }

        // Renomeado e implementado para corresponder à interface (FindById)
        public SalaDTO FindById(long id)
        {
            var entity = _context.Salas.AsNoTracking().FirstOrDefault(s => s.Id == id);
            return entity?.Adapt<SalaDTO>();
        }     

        public List<SalaDTO> FindAll()
        {
            var entities = _context.Salas.AsNoTracking().ToList();
            return entities.Adapt<List<SalaDTO>>();
        }

    }
}
