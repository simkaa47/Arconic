using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arconic.Core.Infrastructure.DataContext.Data.Migrations
{
    /// <inheritdoc />
    public partial class StripCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints");

            migrationBuilder.RenameColumn(
                name: "StripId",
                table: "Strips",
                newName: "StripNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints",
                column: "StripId",
                principalTable: "Strips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints");

            migrationBuilder.RenameColumn(
                name: "StripNumber",
                table: "Strips",
                newName: "StripId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints",
                column: "StripId",
                principalTable: "Strips",
                principalColumn: "Id");
        }
    }
}
