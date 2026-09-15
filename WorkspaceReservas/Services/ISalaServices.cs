using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Utils;

namespace WorkspaceReservas.Services
{
    public interface ISalaServices
    {
        Task<SalaDTO> Create(SalaDTO sala);
        Task<SalaDTO> Update(SalaDTO sala);
        Task<SalaDTO> Delete(long id);
        Task<SalaDTO> FindById(long id);
        Task<List<SalaDTO>  > FindAll();
        Task<SalaDTO> Reativar(long id);
    }
}