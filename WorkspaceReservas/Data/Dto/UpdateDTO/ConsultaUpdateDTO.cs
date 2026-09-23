namespace WorkspaceReservas.Data.Dto.UpdateDTO
{
    public class ConsultaUpdateDTO
    {
        public long? MedicoIdFk { get; set; }
        public string? NomePaciente { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFim { get; set; }
    }
}
