using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Services.Implementations;

namespace WorkspaceReservas.Services
{
    public interface IReservaServices
    {
        Task<ServiceResult<ReservaDTO>> Create(ReservaDTO reserva);
        Task<ReservaDTO> Update(ReservaDTO reserva);
        Task<ReservaDTO> Delete(int id);
        Task<ReservaDTO> FindById(int id);
        Task<List<ReservaDTO>> FindAll();
    }
}
