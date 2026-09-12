using WorkspaceReservas.Data.Dto;

namespace WorkspaceReservas.Services
{
    public interface ISalaServices
    {
        SalaDTO create(SalaDTO sala);
        SalaDTO update(int id, SalaDTO sala);
        SalaDTO delete(int id);
        SalaDTO get(int id);
        List<SalaDTO> list();


    }
}
