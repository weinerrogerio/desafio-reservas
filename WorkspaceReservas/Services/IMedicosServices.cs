using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using FluentResults;
namespace WorkspaceReservas.Services
{
    public interface IMedicosServices
    {
        Task<Result<MedicoDTO>> Create(MedicoDTO medico);
        Task<Result<MedicoDTO>> Update(long id, MedicoUpdateDTO medico);
        //Task<FluentResults.Result<MedicoDTO>> Update(MedicoDTO medico);
        Task<Result<MedicoDTO>> Delete(long id);
        Task<Result<MedicoDTO>> FindById(long id);
        Task<Result<List<MedicoDTO>>> FindAll();
        Task<Result<MedicoDTO>> Reativar(long id);
    }
}
