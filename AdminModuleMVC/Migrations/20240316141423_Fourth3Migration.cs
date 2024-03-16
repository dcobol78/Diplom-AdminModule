using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminModuleMVC.Migrations
{
    /// <inheritdoc />
    public partial class Fourth3Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_CourseFiles_CountryId",
                table: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_CountryId",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Homeworks");

            migrationBuilder.AlterColumn<string>(
                name: "HomeworkFileId",
                table: "Homeworks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_HomeworkFileId",
                table: "Homeworks",
                column: "HomeworkFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_CourseFiles_HomeworkFileId",
                table: "Homeworks",
                column: "HomeworkFileId",
                principalTable: "CourseFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_CourseFiles_HomeworkFileId",
                table: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_HomeworkFileId",
                table: "Homeworks");

            migrationBuilder.AlterColumn<string>(
                name: "HomeworkFileId",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Homeworks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_CountryId",
                table: "Homeworks",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_CourseFiles_CountryId",
                table: "Homeworks",
                column: "CountryId",
                principalTable: "CourseFiles",
                principalColumn: "Id");
        }
    }
}
