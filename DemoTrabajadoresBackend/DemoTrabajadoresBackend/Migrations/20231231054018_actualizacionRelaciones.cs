using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoTrabajadoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Distrito_IdDistrito_IdProvincia",
                table: "Trabajador");

            migrationBuilder.DropIndex(
                name: "IX_Trabajador_IdDistrito_IdProvincia",
                table: "Trabajador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Distrito_1",
                table: "Distrito");

            migrationBuilder.DropColumn(
                name: "IdProvincia",
                table: "Trabajador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Distrito",
                table: "Distrito",
                column: "IdDistrito");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_IdDistrito",
                table: "Trabajador",
                column: "IdDistrito");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Distrito_IdDistrito",
                table: "Trabajador",
                column: "IdDistrito",
                principalTable: "Distrito",
                principalColumn: "IdDistrito");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Distrito_IdDistrito",
                table: "Trabajador");

            migrationBuilder.DropIndex(
                name: "IX_Trabajador_IdDistrito",
                table: "Trabajador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Distrito",
                table: "Distrito");

            migrationBuilder.AddColumn<int>(
                name: "IdProvincia",
                table: "Trabajador",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Distrito_1",
                table: "Distrito",
                columns: new[] { "IdDistrito", "IdProvincia" });

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_IdDistrito_IdProvincia",
                table: "Trabajador",
                columns: new[] { "IdDistrito", "IdProvincia" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Distrito_IdDistrito_IdProvincia",
                table: "Trabajador",
                columns: new[] { "IdDistrito", "IdProvincia" },
                principalTable: "Distrito",
                principalColumns: new[] { "IdDistrito", "IdProvincia" });
        }
    }
}
