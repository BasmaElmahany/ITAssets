using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itassets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeviceMaintainanceSchedule_DeviceID",
                table: "DeviceMaintainanceSchedule",
                column: "DeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceMaintainanceSchedule_Device_DeviceID",
                table: "DeviceMaintainanceSchedule",
                column: "DeviceID",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceMaintainanceSchedule_Device_DeviceID",
                table: "DeviceMaintainanceSchedule");

            migrationBuilder.DropIndex(
                name: "IX_DeviceMaintainanceSchedule_DeviceID",
                table: "DeviceMaintainanceSchedule");
        }
    }
}
