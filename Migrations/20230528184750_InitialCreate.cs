using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId1 = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId2 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CompanyId1",
                        column: x => x.CompanyId1,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salary = table.Column<double>(type: "double", nullable: false),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId1 = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId2 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workers_Companies_CompanyId1",
                        column: x => x.CompanyId1,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "ProjectQualification",
                columns: table => new
                {
                    ProjectsId = table.Column<long>(type: "bigint", nullable: false),
                    QualificationsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectQualification", x => new { x.ProjectsId, x.QualificationsId });
                    table.ForeignKey(
                        name: "FK_ProjectQualification_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectQualification_Qualifications_QualificationsId",
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

            migrationBuilder.CreateTable(
                name: "worker_project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkerId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    date_assigned = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker_project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_worker_project_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_worker_project_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkerQualification",
                columns: table => new
                {
                    WorkerId = table.Column<long>(type: "bigint", nullable: false),
                    QualificationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerQualification", x => new { x.WorkerId, x.QualificationId });
                    table.ForeignKey(
                        name: "FK_WorkerQualification_Qualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerQualification_workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyQualification_QualificationsId",
                table: "CompanyQualification",
                column: "QualificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectQualification_QualificationsId",
                table: "ProjectQualification",
                column: "QualificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId1",
                table: "Projects",
                column: "CompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorker_WorkersId",
                table: "ProjectWorker",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationWorker_WorkersId",
                table: "QualificationWorker",
                column: "WorkersId");

            migrationBuilder.CreateIndex(
                name: "IX_worker_project_ProjectId",
                table: "worker_project",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_worker_project_WorkerId",
                table: "worker_project",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerQualification_QualificationId",
                table: "WorkerQualification",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_workers_CompanyId",
                table: "workers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_workers_CompanyId1",
                table: "workers",
                column: "CompanyId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyQualification");

            migrationBuilder.DropTable(
                name: "ProjectQualification");

            migrationBuilder.DropTable(
                name: "ProjectWorker");

            migrationBuilder.DropTable(
                name: "QualificationWorker");

            migrationBuilder.DropTable(
                name: "worker_project");

            migrationBuilder.DropTable(
                name: "WorkerQualification");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
