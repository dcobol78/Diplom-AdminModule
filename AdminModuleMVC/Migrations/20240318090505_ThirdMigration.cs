using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminModuleMVC.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "HasHomework",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "HasTest",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "HasHomework",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "HasTest",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "HasHomework",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "HasTest",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartingDate",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "Themes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "Sections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SectionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ThemeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFiles_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseFiles_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseFiles_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeworkFileId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_CourseFiles_HomeworkFileId",
                        column: x => x.HomeworkFileId,
                        principalTable: "CourseFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Themes_HomeworkId",
                table: "Themes",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_HomeworkId",
                table: "Sections",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_HomeworkId",
                table: "Courses",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFiles_CourseId",
                table: "CourseFiles",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFiles_SectionId",
                table: "CourseFiles",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFiles_ThemeId",
                table: "CourseFiles",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_HomeworkFileId",
                table: "Homeworks",
                column: "HomeworkFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Homeworks_HomeworkId",
                table: "Courses",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Homeworks_HomeworkId",
                table: "Sections",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Homeworks_HomeworkId",
                table: "Themes",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Homeworks_HomeworkId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Homeworks_HomeworkId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Homeworks_HomeworkId",
                table: "Themes");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "CourseFiles");

            migrationBuilder.DropIndex(
                name: "IX_Themes_HomeworkId",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Sections_HomeworkId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Courses_HomeworkId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Themes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasHomework",
                table: "Themes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTest",
                table: "Themes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasHomework",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTest",
                table: "Sections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasHomework",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTest",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StartingDate",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
