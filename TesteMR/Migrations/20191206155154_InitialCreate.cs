using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteMR.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    nId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cCode = table.Column<string>(maxLength: 30, nullable: false),
                    cDescription = table.Column<string>(maxLength: 150, nullable: true),
                    cUnitCode = table.Column<string>(maxLength: 10, nullable: false),
                    nUnitPrice = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    dCreateDateTime = table.Column<DateTime>(nullable: false),
                    dChangedDateTime = table.Column<DateTime>(nullable: true),
                    uId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.nId);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    nId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cCode = table.Column<string>(maxLength: 10, nullable: false),
                    cDescription = table.Column<string>(maxLength: 30, nullable: true),
                    dCreateDateTime = table.Column<DateTime>(nullable: false),
                    dChangedDateTime = table.Column<DateTime>(nullable: true),
                    uId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.nId);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesArtigos",
                columns: table => new
                {
                    nId = table.Column<int>(nullable: false),
                    nBaseUnitQty = table.Column<decimal>(type: "decimal(20, 10)", nullable: false),
                    nPrice = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    dCreateDateTime = table.Column<DateTime>(nullable: false),
                    dChangedDateTime = table.Column<DateTime>(nullable: true),
                    uId = table.Column<Guid>(nullable: false),
                    uProductId = table.Column<int>(nullable: false),
                    uUnidadeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesArtigos", x => x.nId);
                    table.ForeignKey(
                        name: "FK_UnidadesArtigos_Artigos_nId",
                        column: x => x.nId,
                        principalTable: "Artigos",
                        principalColumn: "nId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadesArtigos_Unidades_nId",
                        column: x => x.nId,
                        principalTable: "Unidades",
                        principalColumn: "nId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnidadesArtigos");

            migrationBuilder.DropTable(
                name: "Artigos");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
