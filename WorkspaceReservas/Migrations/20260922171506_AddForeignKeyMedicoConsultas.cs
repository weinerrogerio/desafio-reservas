using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkspaceReservas.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyMedicoConsultas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Consultas_id_medico_fk",
                table: "Consultas",
                column: "id_medico_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Medicos_id_medico_fk",
                table: "Consultas",
                column: "id_medico_fk",
                principalTable: "Medicos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Medicos_id_medico_fk",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_id_medico_fk",
                table: "Consultas");
        }
    }
}
