﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyManager.Migrations
{
    [DbContext(typeof(CompanyManagerContext))]
    [Migration("20230610202344_IdentityTables")]
    partial class IdentityTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CompanyManager.Models.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CompanyManager.Models.MissingQualification", b =>
                {
                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<long>("QualificationId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("ProjectId", "QualificationId");

                    b.HasIndex("QualificationId");

                    b.ToTable("MissingQualifications");
                });

            modelBuilder.Entity("CompanyManager.Models.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CompanyManager.Models.Qualification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("CompanyManager.Models.Worker", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<double>("Salary")
                        .HasColumnType("double");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("CompanyManager.Models.WorkerProject", b =>
                {
                    b.Property<long>("WorkerId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("DateAssigned")
                        .HasColumnType("longtext")
                        .HasColumnName("date_assigned");

                    b.HasKey("WorkerId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("WorkerProject");
                });

            modelBuilder.Entity("ProjectQualification", b =>
                {
                    b.Property<long>("ProjectsId")
                        .HasColumnType("bigint");

                    b.Property<long>("QualificationsId")
                        .HasColumnType("bigint");

                    b.HasKey("ProjectsId", "QualificationsId");

                    b.HasIndex("QualificationsId");

                    b.ToTable("ProjectQualifications", (string)null);
                });

            modelBuilder.Entity("QualificationWorker", b =>
                {
                    b.Property<long>("QualificationsId")
                        .HasColumnType("bigint");

                    b.Property<long>("WorkersId")
                        .HasColumnType("bigint");

                    b.HasKey("QualificationsId", "WorkersId");

                    b.HasIndex("WorkersId");

                    b.ToTable("WorkerQualification", (string)null);
                });

            modelBuilder.Entity("CompanyManager.Models.MissingQualification", b =>
                {
                    b.HasOne("CompanyManager.Models.Project", "Project")
                        .WithMany("MissingQualifications")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyManager.Models.Qualification", "Qualification")
                        .WithMany()
                        .HasForeignKey("QualificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Qualification");
                });

            modelBuilder.Entity("CompanyManager.Models.Project", b =>
                {
                    b.HasOne("CompanyManager.Models.Company", "Company")
                        .WithMany("Projects")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyManager.Models.Qualification", b =>
                {
                    b.HasOne("CompanyManager.Models.Company", null)
                        .WithMany("Qualifications")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("CompanyManager.Models.Worker", b =>
                {
                    b.HasOne("CompanyManager.Models.Company", "Company")
                        .WithMany("Workers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyManager.Models.WorkerProject", b =>
                {
                    b.HasOne("CompanyManager.Models.Project", "Project")
                        .WithMany("WorkerProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyManager.Models.Worker", "Worker")
                        .WithMany("WorkerProjects")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("ProjectQualification", b =>
                {
                    b.HasOne("CompanyManager.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyManager.Models.Qualification", null)
                        .WithMany()
                        .HasForeignKey("QualificationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QualificationWorker", b =>
                {
                    b.HasOne("CompanyManager.Models.Qualification", null)
                        .WithMany()
                        .HasForeignKey("QualificationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyManager.Models.Worker", null)
                        .WithMany()
                        .HasForeignKey("WorkersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompanyManager.Models.Company", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("Qualifications");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("CompanyManager.Models.Project", b =>
                {
                    b.Navigation("MissingQualifications");

                    b.Navigation("WorkerProjects");
                });

            modelBuilder.Entity("CompanyManager.Models.Worker", b =>
                {
                    b.Navigation("WorkerProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
