using FluentAssertions.Execution;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Npgsql.Internal;
using System.ComponentModel;
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

        public SalaDTO create(SalaDTO sala)
        {
            if (sala == null) return null;
            var entity = sala.Adapt<Sala>();
            _context.Add(entity);
            _context.SaveChanges();
            return entity.Adapt<SalaDTO>();            
        }

        public SalaDTO update(int id, SalaDTO sala)
        {
            throw new NotImplementedException();
        }

        public SalaDTO delete(int id)
        {
            throw new NotImplementedException();
        }

        public SalaDTO get(int id)
        {
            throw new NotImplementedException();
        }

        public List<SalaDTO> list()
        {
            throw new NotImplementedException();
        }

       
    }
}
