namespace WorkspaceReservas.Data.Dto
{
    public class ConsultaDTO
    {
        public int Id { get; set; }
        public int MedicoIdFk { get; set; }
        public required string NomePaciente { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
    }
}
