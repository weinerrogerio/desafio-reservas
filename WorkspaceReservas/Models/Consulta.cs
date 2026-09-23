using System.ComponentModel.DataAnnotations.Schema;
using WorkspaceReservas.Models.Base;

namespace WorkspaceReservas.Models
{
    [Table("Consultas")]
    public class Consulta : BaseEntity
    {
        [Column("id_medico_fk")]
        [ForeignKey("Medico")]
        public long MedicoIdFk { get; set; }

        public Medico Medico { get; set; }

        [Column("nome_paciente")]
        public string NomePaciente { get; set; }

        [Column("data_hora_inicio")]
        public DateTime DataHoraInicio { get; set; }

        [Column("data_hora_fim")]
        public DateTime DataHoraFim { get; set; }
    }
}
