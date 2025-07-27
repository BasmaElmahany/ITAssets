using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itassets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deviceQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "OfficeDeviceAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "EmployeeDeviceAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "OfficeDeviceAssignment");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "EmployeeDeviceAssignment");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Device");
        }
    }
}
