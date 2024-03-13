using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminModuleMVC.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasHomework",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "HasTest",
                table: "Themes");

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

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "Themes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Themes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "Sections",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "CourseFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    AttemptsAlowed = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Themes_HomeworkId",
                table: "Themes",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_TestId",
                table: "Themes",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_HomeworkId",
                table: "Sections",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_TestId",
                table: "Sections",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_HomeworkId",
                table: "Courses",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TestId",
                table: "Courses",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseFiles_HomeworkId",
                table: "CourseFiles",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TestId",
                table: "Question",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFiles_Homeworks_HomeworkId",
                table: "CourseFiles",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Homeworks_HomeworkId",
                table: "Courses",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Tests_TestId",
                table: "Courses",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Homeworks_HomeworkId",
                table: "Sections",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Tests_TestId",
                table: "Sections",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Homeworks_HomeworkId",
                table: "Themes",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Tests_TestId",
                table: "Themes",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFiles_Homeworks_HomeworkId",
                table: "CourseFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Homeworks_HomeworkId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Tests_TestId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Homeworks_HomeworkId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Tests_TestId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Homeworks_HomeworkId",
                table: "Themes");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Tests_TestId",
                table: "Themes");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Themes_HomeworkId",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Themes_TestId",
                table: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Sections_HomeworkId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_TestId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Courses_HomeworkId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TestId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseFiles_HomeworkId",
                table: "CourseFiles");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "HomeworkId",
                table: "CourseFiles");

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
        }
    }
}
