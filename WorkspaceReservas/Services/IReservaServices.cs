using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Utils;

namespace WorkspaceReservas.Services
{
    public interface IReservaServices
    {
        Task<ServiceResult<ReservaDTO>> Create(ReservaDTO reserva);
        Task<ServiceResult<ReservaDTO>> Update(ReservaDTO reserva);
        Task<ReservaDTO> Delete(int id);
        Task<ReservaDTO> FindById(int id);
        Task<List<ReservaDTO>> FindAll();
    }
}
