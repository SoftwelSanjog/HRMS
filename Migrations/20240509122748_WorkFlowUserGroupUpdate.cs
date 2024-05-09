using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class WorkFlowUserGroupUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "WorkFlowUserGroups",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId",
                table: "WorkFlowUserGroups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowUserGroups_ClusterId",
                table: "WorkFlowUserGroups",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowUserGroups_DocumentTypeId",
                table: "WorkFlowUserGroups",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowUserGroups_Clusters_ClusterId",
                table: "WorkFlowUserGroups",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkFlowUserGroups_SystemCodeDetails_DocumentTypeId",
                table: "WorkFlowUserGroups",
                column: "DocumentTypeId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowUserGroups_Clusters_ClusterId",
                table: "WorkFlowUserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkFlowUserGroups_SystemCodeDetails_DocumentTypeId",
                table: "WorkFlowUserGroups");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowUserGroups_ClusterId",
                table: "WorkFlowUserGroups");

            migrationBuilder.DropIndex(
                name: "IX_WorkFlowUserGroups_DocumentTypeId",
                table: "WorkFlowUserGroups");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "WorkFlowUserGroups");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "WorkFlowUserGroups");
        }
    }
}
