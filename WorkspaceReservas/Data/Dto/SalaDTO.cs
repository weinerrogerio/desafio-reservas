namespace WorkspaceReservas.Data.Dto
{
    public class SalaDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int Capacidade { get; set; }
        public decimal PrecoPorHora { get; set; }
        public bool Ativo { get; set; }

    }
}
