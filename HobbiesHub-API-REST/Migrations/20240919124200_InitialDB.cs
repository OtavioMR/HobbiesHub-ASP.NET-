using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbiesHub_API_REST.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    UsuarioDateCadastro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
