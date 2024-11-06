using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arconic.Core.Infrastructure.DataContext.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Thick_points_into_strip_directly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StripId",
                table: "ThickPoints",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeasMode",
                table: "Strips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ThickPoints_StripId",
                table: "ThickPoints",
                column: "StripId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints",
                column: "StripId",
                principalTable: "Strips",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints");

            migrationBuilder.DropIndex(
                name: "IX_ThickPoints_StripId",
                table: "ThickPoints");

            migrationBuilder.DropColumn(
                name: "StripId",
                table: "ThickPoints");

            migrationBuilder.DropColumn(
                name: "MeasMode",
                table: "Strips");
        }
    }
}
