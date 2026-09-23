namespace WorkspaceReservas.Data.Dto
{
    public class ConsultaDTO
    {
        public long Id { get; set; }
        public long MedicoIdFk { get; set; }
        public required string NomePaciente { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
