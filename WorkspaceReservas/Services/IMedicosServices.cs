using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;

namespace WorkspaceReservas.Services
{
    public interface IMedicosServices
    {
        Task<FluentResults.Result<MedicoDTO>> Create(MedicoDTO medico);
        Task<FluentResults.Result<MedicoUpdateDTO>> Update(MedicoUpdateDTO medico);
        Task<FluentResults.Result<MedicoDTO>> Delete(long id);
        Task<FluentResults.Result<MedicoDTO>> FindById(long id);
        Task<FluentResults.Result<List<MedicoDTO>>> FindAll();
        Task<FluentResults.Result<MedicoDTO>> Reativar(long id);
    }
}
