using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Context;

namespace WorkspaceReservas.Services.Implementations
{
    public class ReservaServicesImpl : IReservaServices
    {
        private readonly PostgreSQLContext _context;

        public ReservaServicesImpl(PostgreSQLContext context)
        {
            _context = context;
        }
        public ReservaDTO Create(ReservaDTO reserva)
        {
            if (reserva == null) return null;
            var entity = reserva.Adapt<Reserva>(); // DTO -> Entidade
            _context.Reservas.Add(entity);
            _context.SaveChanges();
            return entity.Adapt<ReservaDTO>(); // Entidade -> DTO
        }

        public ReservaDTO Update(ReservaDTO reserva)
        {
            if (reserva == null) return null;
            var existing = _context.Reservas.Find(reserva.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(reserva);
            _context.SaveChanges();
            return reserva;
        }

        public ReservaDTO Delete(int id)
        {
            var existing = _context.Reservas.Find(id);
            if (existing == null) return null;
            _context.Reservas.Remove(existing);
            _context.SaveChanges();
            return existing.Adapt<ReservaDTO>();
        }

        public ReservaDTO? FindById(int id)
        {
            var entity = _context.Reservas.AsNoTracking().FirstOrDefault(r => r.Id == id);
            return entity?.Adapt<ReservaDTO>();
        }

        public List<ReservaDTO> FindAll()
        {
            var entities = _context.Reservas.AsNoTracking().ToList();
            return entities.Adapt<List<ReservaDTO>>();
        }
    }
}
