using System.ComponentModel.DataAnnotations.Schema;
using WorkspaceReservas.Models.Base;

namespace WorkspaceReservas.Models
{
    [Table("Reservas")]
    public class Reserva : BaseEntity
    {
        [Column("id_sala_fk")]
        [ForeignKey("Sala")]
        public int IdSalaFk { get; set; }

        [Column("nome_cliente", TypeName = "varchar(80)")]
        public required string NomeCliente { get; set; }

        [Column("inicio")]
        public DateTime DataHoraInicio { get; set; }

        [Column("fin")]
        public DateTime DataHoraFim { get; set; }

        [Column("valor_total")]
        public decimal ValorTotal { get; set; }

        [Column("status")]
        public bool status { get; set; }
    }
}
