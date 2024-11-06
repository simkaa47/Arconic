using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arconic.Core.Infrastructure.DataContext.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Thick_Points : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Strips",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StripId = table.Column<string>(type: "TEXT", nullable: false),
                    SteelLabel = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpectedThick = table.Column<float>(type: "REAL", nullable: false),
                    MinExpectedThick = table.Column<float>(type: "REAL", nullable: false),
                    MaxExpectedThick = table.Column<float>(type: "REAL", nullable: false),
                    ExpectedWidth = table.Column<float>(type: "REAL", nullable: false),
                    MinExpectedWidth = table.Column<float>(type: "REAL", nullable: false),
                    MaxExpectedWidth = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Width = table.Column<float>(type: "REAL", nullable: false),
                    Klin = table.Column<float>(type: "REAL", nullable: false),
                    Chechewitsa = table.Column<float>(type: "REAL", nullable: false),
                    StripId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scans_Strips_StripId",
                        column: x => x.StripId,
                        principalTable: "Strips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThickPoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Thick = table.Column<float>(type: "REAL", nullable: false),
                    Lendth = table.Column<float>(type: "REAL", nullable: false),
                    Position = table.Column<float>(type: "REAL", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ScanId = table.Column<long>(type: "INTEGER", nullable: false),
                    Speed = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThickPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThickPoints_Scans_ScanId",
                        column: x => x.ScanId,
                        principalTable: "Scans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scans_StripId",
                table: "Scans",
                column: "StripId");

            migrationBuilder.CreateIndex(
                name: "IX_ThickPoints_ScanId",
                table: "ThickPoints",
                column: "ScanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThickPoints");

            migrationBuilder.DropTable(
                name: "Scans");

            migrationBuilder.DropTable(
                name: "Strips");
        }
    }
}
