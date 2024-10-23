using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbiesHub_API_REST.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGrupo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryGrupo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionGrupo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LimiteUsuariosGrupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UsuarioEmail = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioNameSystem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UsuarioSenhaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UsuarioAge = table.Column<int>(type: "int", nullable: false),
                    UsuarioDateCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
