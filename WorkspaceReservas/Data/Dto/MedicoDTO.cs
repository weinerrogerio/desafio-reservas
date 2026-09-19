using System.ComponentModel.DataAnnotations;

namespace WorkspaceReservas.Data.Dto
{
    public class MedicoDTO
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "O nome do médico é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O CRM do médico é obrigatório.")]
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
