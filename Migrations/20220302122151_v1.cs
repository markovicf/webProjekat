using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BolnicaProjekat.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bolnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pacijenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    JMBG = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacijenti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Odeljenja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specijalizacija = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BolnicaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odeljenja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Odeljenja_Bolnice_BolnicaID",
                        column: x => x.BolnicaID,
                        principalTable: "Bolnice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doktori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OdeljenjeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktori", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Doktori_Odeljenja_OdeljenjeID",
                        column: x => x.OdeljenjeID,
                        principalTable: "Odeljenja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Spojevi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoktorID = table.Column<int>(type: "int", nullable: true),
                    PacijentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spojevi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Spojevi_Doktori_DoktorID",
                        column: x => x.DoktorID,
                        principalTable: "Doktori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spojevi_Pacijenti_PacijentID",
                        column: x => x.PacijentID,
                        principalTable: "Pacijenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktori_OdeljenjeID",
                table: "Doktori",
                column: "OdeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Odeljenja_BolnicaID",
                table: "Odeljenja",
                column: "BolnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Spojevi_DoktorID",
                table: "Spojevi",
                column: "DoktorID");

            migrationBuilder.CreateIndex(
                name: "IX_Spojevi_PacijentID",
                table: "Spojevi",
                column: "PacijentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spojevi");

            migrationBuilder.DropTable(
                name: "Doktori");

            migrationBuilder.DropTable(
                name: "Pacijenti");

            migrationBuilder.DropTable(
                name: "Odeljenja");

            migrationBuilder.DropTable(
                name: "Bolnice");
        }
    }
}
