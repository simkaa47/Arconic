using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arconic.Core.Infrastructure.DataContext.Data.Migrations
{
    /// <inheritdoc />
    public partial class StripCascadeDelete1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints",
                column: "ScanId",
                principalTable: "Scans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints");

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints",
                column: "ScanId",
                principalTable: "Scans",
                principalColumn: "Id");
        }
    }
}
