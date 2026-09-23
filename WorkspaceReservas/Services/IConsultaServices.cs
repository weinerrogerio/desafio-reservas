using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;

namespace WorkspaceReservas.Services
{
    public interface IConsultaServices
    {
        Task<FluentResults.Result<ConsultaDTO>> Create(ConsultaDTO consulta);
        Task<FluentResults.Result<ConsultaDTO>> Update(long id, ConsultaUpdateDTO consulta);
        Task<FluentResults.Result<ConsultaDTO>> FindById(long id);
        Task<FluentResults.Result<List<ConsultaDTO>>> FindAll();
        Task<FluentResults.Result<ConsultaDTO>> Delete(long id);
    }
}
