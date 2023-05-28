using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId2",
                table: "workers");

            migrationBuilder.DropColumn(
                name: "CompanyId2",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId2",
                table: "workers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CompanyId2",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
