using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkspaceReservas.Migrations
{
    /// <inheritdoc />
    public partial class alterTableCosultaIntToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "id_medico_fk",
                table: "Consultas",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "id_medico_fk",
                table: "Consultas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
