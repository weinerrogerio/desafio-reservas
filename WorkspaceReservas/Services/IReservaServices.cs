using WorkspaceReservas.Data.Dto;

namespace WorkspaceReservas.Services
{
    public interface IReservaServices
    {
        ReservaDTO create(ReservaDTO reserva);
        ReservaDTO update(int id, ReservaDTO reserva);
        ReservaDTO delete(int id);
        ReservaDTO get(int id);
        List<ReservaDTO> list();
    }
}
