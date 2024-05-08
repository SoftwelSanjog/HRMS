using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class LeaveAdjustmentTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "LeaveAdjustmentEntries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "LeaveAdjustmentEntries",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "LeaveAdjustmentEntries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "LeaveAdjustmentEntries",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "LeaveAdjustmentEntries");
        }
    }
}
