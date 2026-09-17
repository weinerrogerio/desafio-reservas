using System.ComponentModel.DataAnnotations.Schema;
using WorkspaceReservas.Models.Base;

namespace WorkspaceReservas.Models
{
    [Table("Medicos")]
    public class Medico : BaseEntity
    {
        [Column("nome")]
        public string Nome { get; set; }
        [Column("crm")]
        public string CRM { get; set; }
        [Column("especialidade")]
        public string Especialidade { get; set; }
        [Column("ativo")]
        public bool Ativo { get; set; } = true;
    }
}
