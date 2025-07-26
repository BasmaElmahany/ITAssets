using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Itassets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class request_transfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceCount = table.Column<int>(type: "int", nullable: false),
                    officeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceRequests_Category_categoryID",
                        column: x => x.categoryID,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceRequests_Office_officeId",
                        column: x => x.officeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTransfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldEmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewEmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOnly = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTransfer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRequests_categoryID",
                table: "DeviceRequests",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRequests_officeId",
                table: "DeviceRequests",
                column: "officeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceRequests");

            migrationBuilder.DropTable(
                name: "DeviceTransfer");
        }
    }
}
