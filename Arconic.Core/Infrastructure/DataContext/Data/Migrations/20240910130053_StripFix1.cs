using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arconic.Core.Infrastructure.DataContext.Data.Migrations
{
    /// <inheritdoc />
    public partial class StripFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<long>(
                name: "StripId",
                table: "ThickPoints",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ThickPoints_Strips_StripId",
                table: "ThickPoints",
                column: "StripId",
                principalTable: "Strips",
                principalColumn: "Id");
        }
    }
}
