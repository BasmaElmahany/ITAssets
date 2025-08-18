using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itassets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deviceSpex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Spex",
                table: "Device",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Warranty",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Spex",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "Warranty",
                table: "Device");
        }
    }
}
