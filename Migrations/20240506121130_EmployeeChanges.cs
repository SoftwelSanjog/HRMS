using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cluster",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DesignationId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ClusterId",
                table: "Employees",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryId",
                table: "Employees",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenderId",
                table: "Employees",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Clusters_ClusterId",
                table: "Employees",
                column: "ClusterId",
                principalTable: "Clusters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Designations_DesignationId",
                table: "Employees",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_GenderId",
                table: "Employees",
                column: "GenderId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Clusters_ClusterId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Designations_DesignationId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_GenderId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ClusterId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CountryId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DesignationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GenderId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Cluster",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
