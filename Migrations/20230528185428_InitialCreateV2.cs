using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CompanyId1",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_workers_Companies_CompanyId1",
                table: "workers");

            migrationBuilder.DropIndex(
                name: "IX_workers_CompanyId1",
                table: "workers");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CompanyId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId1",
                table: "workers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId1",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_workers_CompanyId1",
                table: "workers",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId1",
                table: "Projects",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CompanyId1",
                table: "Projects",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workers_Companies_CompanyId1",
                table: "workers",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
