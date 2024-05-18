﻿// <auto-generated />
using System;
using AdminModuleMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdminModuleMVC.Migrations
{
    [DbContext(typeof(CourseDbContext))]
    [Migration("20240318090505_ThirdMigration")]
    partial class ThirdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdminModuleMVC.Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AutorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AutorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("HomeworkId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsCoherent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.CourseFile", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SectionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ThemeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("SectionId");

                    b.HasIndex("ThemeId");

                    b.ToTable("CourseFiles");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Homework", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("HomeworkFileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkFileId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Section", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("HomeworkId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdCourse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("HomeworkId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Theme", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("HomeworkId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdSection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("SectionId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("SectionId");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Course", b =>
                {
                    b.HasOne("AdminModuleMVC.Models.Homework", "Homework")
                        .WithMany()
                        .HasForeignKey("HomeworkId");

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.CourseFile", b =>
                {
                    b.HasOne("AdminModuleMVC.Models.Course", null)
                        .WithMany("CourseFiles")
                        .HasForeignKey("CourseId");

                    b.HasOne("AdminModuleMVC.Models.Section", null)
                        .WithMany("SectionFiles")
                        .HasForeignKey("SectionId");

                    b.HasOne("AdminModuleMVC.Models.Theme", null)
                        .WithMany("ThemeFiles")
                        .HasForeignKey("ThemeId");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Homework", b =>
                {
                    b.HasOne("AdminModuleMVC.Models.CourseFile", "HomeWorkFile")
                        .WithMany()
                        .HasForeignKey("HomeworkFileId");

                    b.Navigation("HomeWorkFile");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Section", b =>
                {
                    b.HasOne("AdminModuleMVC.Models.Course", null)
                        .WithMany("Sections")
                        .HasForeignKey("CourseId");

                    b.HasOne("AdminModuleMVC.Models.Homework", "Homework")
                        .WithMany()
                        .HasForeignKey("HomeworkId");

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Theme", b =>
                {
                    b.HasOne("AdminModuleMVC.Models.Homework", "Homework")
                        .WithMany()
                        .HasForeignKey("HomeworkId");

                    b.HasOne("AdminModuleMVC.Models.Section", null)
                        .WithMany("Themes")
                        .HasForeignKey("SectionId");

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Course", b =>
                {
                    b.Navigation("CourseFiles");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Section", b =>
                {
                    b.Navigation("SectionFiles");

                    b.Navigation("Themes");
                });

            modelBuilder.Entity("AdminModuleMVC.Models.Theme", b =>
                {
                    b.Navigation("ThemeFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
