using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkspaceReservas.Models.Base;

namespace WorkspaceReservas.Models
{
    [Table("Medicos")]
    public class Medico : BaseEntity
    {
        [Column("nome")]
        //[Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Column("crm")]
        //[Required(ErrorMessage = "CRM é obrigatório")]
        public string CRM { get; set; }

        [Column("especialidade")]
        public string Especialidade { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; } = true;

        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}
