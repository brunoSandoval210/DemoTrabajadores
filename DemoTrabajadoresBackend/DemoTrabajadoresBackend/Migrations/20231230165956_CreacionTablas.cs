using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoTrabajadoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    IdDepartamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDepartamento = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.IdDepartamento);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    IdProvincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDepartamento = table.Column<int>(type: "int", nullable: false),
                    NombreProvincia = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.IdProvincia);
                    table.ForeignKey(
                        name: "FK_Provincia_Departamento_IdDepartamento",
                        column: x => x.IdDepartamento,
                        principalTable: "Departamento",
                        principalColumn: "IdDepartamento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distrito",
                columns: table => new
                {
                    IdDistrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProvincia = table.Column<int>(type: "int", nullable: false),
                    NombreDistrito = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distrito_1", x => new { x.IdDistrito, x.IdProvincia });
                    table.ForeignKey(
                        name: "FK_Distrito_Provincia_IdProvincia",
                        column: x => x.IdProvincia,
                        principalTable: "Provincia",
                        principalColumn: "IdProvincia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    IdTrabajador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDocumento = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    NroDocumento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Nombres = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Sexo = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    IdDistrito = table.Column<int>(type: "int", nullable: true),
                    IdProvincia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.IdTrabajador);
                    table.ForeignKey(
                        name: "FK_Trabajador_Distrito_IdDistrito_IdProvincia",
                        columns: x => new { x.IdDistrito, x.IdProvincia },
                        principalTable: "Distrito",
                        principalColumns: new[] { "IdDistrito", "IdProvincia" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distrito_IdProvincia",
                table: "Distrito",
                column: "IdProvincia");

            migrationBuilder.CreateIndex(
                name: "IX_Provincia_IdDepartamento",
                table: "Provincia",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_IdDistrito_IdProvincia",
                table: "Trabajador",
                columns: new[] { "IdDistrito", "IdProvincia" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Distrito");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropTable(
                name: "Departamento");
        }
    }
}
