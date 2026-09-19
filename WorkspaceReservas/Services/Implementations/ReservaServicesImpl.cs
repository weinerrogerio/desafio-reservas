using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Context;
using WorkspaceReservas.Utils;

namespace WorkspaceReservas.Services.Implementations
{
    public class ReservaServicesImpl : IReservaServices
    {
        private readonly PostgreSQLContext _context;

        public ReservaServicesImpl(PostgreSQLContext context)
        {
            _context = context;
        }
        public async Task<ServiceResult<ReservaDTO>> Create(ReservaDTO reserva)
        {
            bool existeConflito = await _context.Reservas
                .AnyAsync(r => r.IdSalaFk == reserva.IdSalaFk &&
                               reserva.DataHoraInicio < r.DataHoraFim &&
                               reserva.DataHoraFim > r.DataHoraInicio);

            if ( existeConflito )
                return ServiceResult<ReservaDTO>.Fail("Sala", "Já existe uma reserva nesse horário para essa sala.");

            var sala = await _context.Salas.FindAsync(reserva.IdSalaFk);
            if ( sala == null )
                return ServiceResult<ReservaDTO>.Fail("IdSalaFk", "Sala não encontrada.");

            var duracaoHoras = ( reserva.DataHoraFim - reserva.DataHoraInicio ).TotalHours;
            reserva.ValorTotal = ( decimal ) duracaoHoras * sala.PrecoPorHora;

            var entity = reserva.Adapt<Reserva>();
            _context.Reservas.Add(entity);
            await _context.SaveChangesAsync();
            return ServiceResult<ReservaDTO>.Ok(entity.Adapt<ReservaDTO>());
        }

        public async Task<ServiceResult<ReservaDTO>> Update(ReservaDTO reserva)
        {
            if ( reserva == null )
                return ServiceResult<ReservaDTO>.Fail("Reserva", "Dados da reserva não informados.");

            var existing = await _context.Reservas.FindAsync(reserva.Id);
            if ( existing == null )
                return ServiceResult<ReservaDTO>.Fail("Id", "Reserva não encontrada.");

            _context.Entry(existing).CurrentValues.SetValues(reserva);
            await _context.SaveChangesAsync();
            return ServiceResult<ReservaDTO>.Ok(existing.Adapt<ReservaDTO>());
        }

        public async Task<ReservaDTO> Delete(int id)
        {
            var existing = await _context.Reservas.FindAsync(id);
            if ( existing == null ) return null;
            _context.Reservas.Remove(existing);
            await _context.SaveChangesAsync();
            return existing.Adapt<ReservaDTO>();
        }

        public async Task<ReservaDTO> FindById(int id)
        {
            var entity = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            return entity?.Adapt<ReservaDTO>();
        }

        public async Task<List<ReservaDTO>> FindAll()
        {
            var entities = await _context.Reservas.AsNoTracking().ToListAsync();
            return entities.Adapt<List<ReservaDTO>>();
        }
    }
}
//    public class ServiceResult<T>
//    {
//        public bool Success { get; private set; }
//        public T? Data { get; private set; }
//        public Dictionary<string, string[]> Errors { get; private set; } = new();

//        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };

//        public static ServiceResult<T> Fail(string campo, string mensagem) => new()
//        {
//            Success = false,
//            Errors = new Dictionary<string, string[]> { { campo, new[] { mensagem } } }
//        };
//    }
//}
