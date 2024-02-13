using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreeTierArch.UI.Migrations
{
    /// <inheritdoc />
    public partial class Update_State_Configuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "States",
                type: "Varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_States_Name",
                table: "States",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_States_Name",
                table: "States");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "States",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(100)");
        }
    }
}
