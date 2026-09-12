namespace WorkspaceReservas.Data.Dto
{
    public class ReservaDTO
    {
        public int IdSalaFk { get; set; }
        public required string NomeCliente { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public decimal ValorTotal { get; set; }
        public bool status { get; set; }
    }
}
