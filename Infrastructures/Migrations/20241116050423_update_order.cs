using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
                table: "Order",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "Order");
        }
    }
}
