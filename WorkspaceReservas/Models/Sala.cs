using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkspaceReservas.Models.Base;

namespace WorkspaceReservas.Models
{
    [Table("sala")]
    public class Sala : BaseEntity
    {
        [Column("nome", TypeName = "varchar(80)")]        
        [Required(ErrorMessage = "Name is required")]        
        public required string Nome { get; set; }

        [Column("capacidade")]
        [Required(ErrorMessage = "Capacity is required")]
        public int Capacidade { get; set; }

        [Column("preco_por_hora")]
        [Required(ErrorMessage = "Price per hour is required")]
        public decimal PrecoPorHora { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }

    }
}
