using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class ApprovalUserMatrixs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalUserMatrixs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    WorkFlowUserGroupId = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalUserMatrixs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovalUserMatrixs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalUserMatrixs_SystemCodeDetails_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "SystemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApprovalUserMatrixs_WorkFlowUserGroups_WorkFlowUserGroupId",
                        column: x => x.WorkFlowUserGroupId,
                        principalTable: "WorkFlowUserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalUserMatrixs_DocumentTypeId",
                table: "ApprovalUserMatrixs",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalUserMatrixs_UserId",
                table: "ApprovalUserMatrixs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalUserMatrixs_WorkFlowUserGroupId",
                table: "ApprovalUserMatrixs",
                column: "WorkFlowUserGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalUserMatrixs");
        }
    }
}
