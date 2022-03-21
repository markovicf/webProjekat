using Microsoft.EntityFrameworkCore.Migrations;

namespace BolnicaProjekat.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JMBG",
                table: "Pacijenti");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JMBG",
                table: "Pacijenti",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
