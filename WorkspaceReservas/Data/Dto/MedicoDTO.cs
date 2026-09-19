using System.ComponentModel.DataAnnotations;

namespace WorkspaceReservas.Data.Dto
{
    public class MedicoDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
