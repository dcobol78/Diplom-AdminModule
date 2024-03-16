using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminModuleMVC.Migrations
{
    /// <inheritdoc />
    public partial class Fourth1Migration : Migration
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
                table: "Sections",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeworkId",
                table: "Courses",
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeWorkFileId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_CourseFiles_HomeWorkFileId",
                        column: x => x.HomeWorkFileId,
                        principalTable: "CourseFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_HomeworkId",
                table: "Sections",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_HomeworkId",
                table: "Courses",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_HomeWorkFileId",
                table: "Homeworks",
                column: "HomeWorkFileId");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_Sections_HomeworkId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Courses_HomeworkId",
                table: "Courses");

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
