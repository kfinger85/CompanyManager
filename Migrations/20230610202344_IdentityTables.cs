using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Migrations
{
    /// <inheritdoc />
    public partial class IdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQualification_Projects_ProjectsId",
                table: "ProjectQualification");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQualification_Qualifications_QualificationsId",
                table: "ProjectQualification");

            migrationBuilder.DropForeignKey(
                name: "FK_worker_project_Projects_ProjectId",
                table: "worker_project");

            migrationBuilder.DropForeignKey(
                name: "FK_worker_project_workers_WorkerId",
                table: "worker_project");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerQualification_Qualifications_QualificationId",
                table: "WorkerQualification");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerQualification_workers_WorkerId",
                table: "WorkerQualification");

            migrationBuilder.DropForeignKey(
                name: "FK_workers_Companies_CompanyId",
                table: "workers");

            migrationBuilder.DropTable(
                name: "CompanyQualification");

            migrationBuilder.DropTable(
                name: "ProjectWorker");

            migrationBuilder.DropTable(
                name: "QualificationWorker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_workers",
                table: "workers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_worker_project",
                table: "worker_project");

            migrationBuilder.DropIndex(
                name: "IX_worker_project_WorkerId",
                table: "worker_project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectQualification",
                table: "ProjectQualification");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "worker_project");

            migrationBuilder.RenameTable(
                name: "workers",
                newName: "Workers");

            migrationBuilder.RenameTable(
                name: "worker_project",
                newName: "WorkerProject");

            migrationBuilder.RenameTable(
                name: "ProjectQualification",
                newName: "ProjectQualifications");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Workers",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_workers_CompanyId",
                table: "Workers",
                newName: "IX_Workers_CompanyId");

            migrationBuilder.RenameColumn(
                name: "QualificationId",
                table: "WorkerQualification",
                newName: "WorkersId");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "WorkerQualification",
                newName: "QualificationsId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerQualification_QualificationId",
                table: "WorkerQualification",
                newName: "IX_WorkerQualification_WorkersId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Qualifications",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_worker_project_ProjectId",
                table: "WorkerProject",
                newName: "IX_WorkerProject_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectQualification_QualificationsId",
                table: "ProjectQualifications",
                newName: "IX_ProjectQualifications_QualificationsId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Workers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Workers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Workers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Qualifications",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Qualifications",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Projects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Projects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "date_assigned",
                table: "WorkerProject",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workers",
                table: "Workers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkerProject",
                table: "WorkerProject",
                columns: new[] { "WorkerId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectQualifications",
                table: "ProjectQualifications",
                columns: new[] { "ProjectsId", "QualificationsId" });

            migrationBuilder.CreateTable(
                name: "MissingQualifications",
                columns: table => new
                {
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    QualificationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissingQualifications", x => new { x.ProjectId, x.QualificationId });
                    table.ForeignKey(
                        name: "FK_MissingQualifications_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissingQualifications_Qualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_CompanyId",
                table: "Qualifications",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingQualifications_QualificationId",
                table: "MissingQualifications",
                column: "QualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQualifications_Projects_ProjectsId",
                table: "ProjectQualifications",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQualifications_Qualifications_QualificationsId",
                table: "ProjectQualifications",
                column: "QualificationsId",
                principalTable: "Qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_Companies_CompanyId",
                table: "Qualifications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProject_Projects_ProjectId",
                table: "WorkerProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerProject_Workers_WorkerId",
                table: "WorkerProject",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerQualification_Qualifications_QualificationsId",
                table: "WorkerQualification",
                column: "QualificationsId",
                principalTable: "Qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerQualification_Workers_WorkersId",
                table: "WorkerQualification",
                column: "WorkersId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Companies_CompanyId",
                table: "Workers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQualifications_Projects_ProjectsId",
                table: "ProjectQualifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectQualifications_Qualifications_QualificationsId",
                table: "ProjectQualifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_Companies_CompanyId",
                table: "Qualifications");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProject_Projects_ProjectId",
                table: "WorkerProject");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerProject_Workers_WorkerId",
                table: "WorkerProject");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerQualification_Qualifications_QualificationsId",
                table: "WorkerQualification");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerQualification_Workers_WorkersId",
                table: "WorkerQualification");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Companies_CompanyId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "MissingQualifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workers",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Qualifications_CompanyId",
                table: "Qualifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkerProject",
                table: "WorkerProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectQualifications",
                table: "ProjectQualifications");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Qualifications");

            migrationBuilder.RenameTable(
                name: "Workers",
                newName: "workers");

            migrationBuilder.RenameTable(
                name: "WorkerProject",
                newName: "worker_project");

            migrationBuilder.RenameTable(
                name: "ProjectQualifications",
                newName: "ProjectQualification");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "workers",
                newName: "username");

            migrationBuilder.RenameIndex(
                name: "IX_Workers_CompanyId",
                table: "workers",
                newName: "IX_workers_CompanyId");

            migrationBuilder.RenameColumn(
                name: "WorkersId",
                table: "WorkerQualification",
                newName: "QualificationId");

            migrationBuilder.RenameColumn(
                name: "QualificationsId",
                table: "WorkerQualification",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerQualification_WorkersId",
                table: "WorkerQualification",
                newName: "IX_WorkerQualification_QualificationId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Qualifications",
                newName: "name");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerProject_ProjectId",
                table: "worker_project",
                newName: "IX_worker_project_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectQualifications_QualificationsId",
                table: "ProjectQualification",
                newName: "IX_ProjectQualification_QualificationsId");

            migrationBuilder.UpdateData(
                table: "workers",
                keyColumn: "username",
                keyValue: null,
                column: "username",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "workers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "workers",
                keyColumn: "Password",
                keyValue: null,
                column: "Password",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "workers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "workers",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "workers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Qualifications",
                keyColumn: "name",
                keyValue: null,
                column: "name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Qualifications",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Projects",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "worker_project",
                keyColumn: "date_assigned",
                keyValue: null,
                column: "date_assigned",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "date_assigned",
                table: "worker_project",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "worker_project",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_workers",
                table: "workers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_worker_project",
                table: "worker_project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectQualification",
                table: "ProjectQualification",
                columns: new[] { "ProjectsId", "QualificationsId" });

            migrationBuilder.CreateTable(
                name: "CompanyQualification",
                columns: table => new
                {
                    CompaniesId = table.Column<long>(type: "bigint", nullable: false),
                    QualificationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyQualification", x => new { x.CompaniesId, x.QualificationsId });
                    table.ForeignKey(
                        name: "FK_CompanyQualification_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyQualification_Qualifications_QualificationsId",
                        column: x => x.QualificationsId,
                        principalTable: "Qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProjectWorker",
                columns: table => new
                {
                    ProjectsId = table.Column<long>(type: "bigint", nullable: false),
                    WorkersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWorker", x => new { x.ProjectsId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_ProjectWorker_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectWorker_workers_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QualificationWorker",
                columns: table => new
                {
                    QualificationsId = table.Column<long>(type: "bigint", nullable: false),
                    WorkersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationWorker", x => new { x.QualificationsId, x.WorkersId });
                    table.ForeignKey(
                        name: "FK_QualificationWorker_Qualifications_QualificationsId",
                        column: x => x.QualificationsId,
                        principalTable: "Qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualificationWorker_workers_WorkersId",
                        column: x => x.WorkersId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_worker_project_WorkerId",
                table: "worker_project",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyQualification_QualificationsId",
                table: "CompanyQualification",
                column: "QualificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorker_WorkersId",
                table: "ProjectWorker",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationWorker_WorkersId",
                table: "QualificationWorker",
                column: "WorkersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQualification_Projects_ProjectsId",
                table: "ProjectQualification",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectQualification_Qualifications_QualificationsId",
                table: "ProjectQualification",
                column: "QualificationsId",
                principalTable: "Qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_worker_project_Projects_ProjectId",
                table: "worker_project",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_worker_project_workers_WorkerId",
                table: "worker_project",
                column: "WorkerId",
                principalTable: "workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerQualification_Qualifications_QualificationId",
                table: "WorkerQualification",
                column: "QualificationId",
                principalTable: "Qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerQualification_workers_WorkerId",
                table: "WorkerQualification",
                column: "WorkerId",
                principalTable: "workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_workers_Companies_CompanyId",
                table: "workers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
