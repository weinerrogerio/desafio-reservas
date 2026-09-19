using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;

namespace WorkspaceReservas.Models.Context
{
    public class PostgreSQLContext : DbContext
    {
        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options) { }

        public DbSet<Sala> Salas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aplica automaticamente todas as classes IEntityTypeConfiguration do projeto
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSQLContext).Assembly);
        }
    }
}
