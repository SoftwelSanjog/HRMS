using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class LeaveAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveAdjustmentEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    NoOfDays = table.Column<decimal>(type: "numeric", nullable: false),
                    LeaveAdjustmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeaveStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LeaveEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdjustmentDescription = table.Column<string>(type: "text", nullable: false),
                    AdjustmentTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAdjustmentEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveAdjustmentEntries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveAdjustmentEntries_SystemCodeDetails_AdjustmentTypeId",
                        column: x => x.AdjustmentTypeId,
                        principalTable: "SystemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_AdjustmentTypeId",
                table: "LeaveAdjustmentEntries",
                column: "AdjustmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_EmployeeId",
                table: "LeaveAdjustmentEntries",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveAdjustmentEntries");
        }
    }
}
