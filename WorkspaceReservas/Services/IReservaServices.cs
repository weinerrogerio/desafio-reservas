using WorkspaceReservas.Data.Dto;

namespace WorkspaceReservas.Services
{
    public interface IReservaServices
    {
        ReservaDTO Create(ReservaDTO reserva);
        ReservaDTO Update(ReservaDTO reserva);
        ReservaDTO Delete(int id);
        ReservaDTO FindById(int id);
        List<ReservaDTO> FindAll();
    }
}
