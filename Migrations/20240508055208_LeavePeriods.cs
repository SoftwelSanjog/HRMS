using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class LeavePeriods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeavePeriod",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.AddColumn<int>(
                name: "LeavePeriodId",
                table: "LeaveAdjustmentEntries",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LeavePeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Closed = table.Column<bool>(type: "boolean", nullable: false),
                    Locked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavePeriods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_LeavePeriodId",
                table: "LeaveAdjustmentEntries",
                column: "LeavePeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAdjustmentEntries_LeavePeriods_LeavePeriodId",
                table: "LeaveAdjustmentEntries",
                column: "LeavePeriodId",
                principalTable: "LeavePeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAdjustmentEntries_LeavePeriods_LeavePeriodId",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropTable(
                name: "LeavePeriods");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAdjustmentEntries_LeavePeriodId",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropColumn(
                name: "LeavePeriodId",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.AddColumn<string>(
                name: "LeavePeriod",
                table: "LeaveAdjustmentEntries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
