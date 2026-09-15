namespace WorkspaceReservas.Data.Dto
{
    public class SalaDTO
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public int Capacidade { get; set; }

        public decimal PrecoPorHora { get; set; }

        public bool Ativo { get; set; } = true;
    }
}