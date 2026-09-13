using WorkspaceReservas.Data.Dto;

namespace WorkspaceReservas.Services
{
    public interface ISalaServices
    {
        SalaDTO Create(SalaDTO sala);
        SalaDTO Update(SalaDTO sala);
        SalaDTO Delete(long id);
        SalaDTO FindById(long id);
        List<SalaDTO> FindAll();


    }
}
