using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itassets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeviceTransfer_DeviceID",
                table: "DeviceTransfer",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTransfer_NewEmpId",
                table: "DeviceTransfer",
                column: "NewEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTransfer_OldEmpId",
                table: "DeviceTransfer",
                column: "OldEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTransfer_Device_DeviceID",
                table: "DeviceTransfer",
                column: "DeviceID",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTransfer_Employee_NewEmpId",
                table: "DeviceTransfer",
                column: "NewEmpId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTransfer_Employee_OldEmpId",
                table: "DeviceTransfer",
                column: "OldEmpId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTransfer_Device_DeviceID",
                table: "DeviceTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTransfer_Employee_NewEmpId",
                table: "DeviceTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTransfer_Employee_OldEmpId",
                table: "DeviceTransfer");

            migrationBuilder.DropIndex(
                name: "IX_DeviceTransfer_DeviceID",
                table: "DeviceTransfer");

            migrationBuilder.DropIndex(
                name: "IX_DeviceTransfer_NewEmpId",
                table: "DeviceTransfer");

            migrationBuilder.DropIndex(
                name: "IX_DeviceTransfer_OldEmpId",
                table: "DeviceTransfer");
        }
    }
}
