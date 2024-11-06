using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arconic.Core.Infrastructure.DataContext.Data.Migrations
{
    /// <inheritdoc />
    public partial class StripFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints");

            migrationBuilder.AlterColumn<long>(
                name: "StripId",
                table: "ThickPoints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<long>(
                name: "ScanId",
                table: "ThickPoints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints",
                column: "ScanId",
                principalTable: "Scans",
                principalColumn: "Id");

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
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints");

            migrationBuilder.AlterColumn<long>(
                name: "StripId",
                table: "ThickPoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ScanId",
                table: "ThickPoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Scans_ScanId",
                table: "ThickPoints",
                column: "ScanId",
                principalTable: "Scans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints",
                column: "StripId",
                principalTable: "Strips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
