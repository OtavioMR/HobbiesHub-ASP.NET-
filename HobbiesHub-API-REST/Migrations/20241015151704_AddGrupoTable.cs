using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbiesHub_API_REST.Migrations
{
    public partial class AddGrupoTable : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupos");
        }
    }
}
