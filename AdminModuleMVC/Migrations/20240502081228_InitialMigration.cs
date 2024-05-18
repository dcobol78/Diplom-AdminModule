using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminModuleMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFiles_Sections_SectionId",
                table: "CourseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Sections_SectionId",
                table: "Themes");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Themes",
                newName: "SectorId");

            migrationBuilder.RenameIndex(
                name: "IX_Themes_SectionId",
                table: "Themes",
                newName: "IX_Themes_SectorId");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "CourseFiles",
                newName: "SectorId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFiles_SectionId",
                table: "CourseFiles",
                newName: "IX_CourseFiles_SectorId");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Themes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseTime",
                table: "Tests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ParentType",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Tests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Sections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Homeworks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ParentType",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Homeworks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ParentType",
                table: "CourseFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdCourse = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CourseId",
                table: "Events",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFiles_Sections_SectorId",
                table: "CourseFiles",
                column: "SectorId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Sections_SectorId",
                table: "Themes",
                column: "SectorId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFiles_Sections_SectorId",
                table: "CourseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Sections_SectorId",
                table: "Themes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ParentType",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "ParentType",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "StartingDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ParentType",
                table: "CourseFiles");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "Themes",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Themes_SectorId",
                table: "Themes",
                newName: "IX_Themes_SectionId");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "CourseFiles",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseFiles_SectorId",
                table: "CourseFiles",
                newName: "IX_CourseFiles_SectionId");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Themes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFiles_Sections_SectionId",
                table: "CourseFiles",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Sections_SectionId",
                table: "Themes",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }
    }
}
