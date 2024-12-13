using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class add_account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isConfirm",
                table: "Account",
                newName: "IsConfirm");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmToken",
                table: "Account",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmToken",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "IsConfirm",
                table: "Account",
                newName: "isConfirm");
        }
    }
}
