using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminModuleMVC.Migrations
{
    /// <inheritdoc />
    public partial class Fourth2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_CourseFiles_HomeWorkFileId",
                table: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_HomeWorkFileId",
                table: "Homeworks");

            migrationBuilder.RenameColumn(
                name: "HomeWorkFileId",
                table: "Homeworks",
                newName: "HomeworkFileId");

            migrationBuilder.AlterColumn<string>(
                name: "HomeworkFileId",
                table: "Homeworks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "HomeworkFileId",
                table: "Homeworks",
                newName: "HomeWorkFileId");

            migrationBuilder.AlterColumn<string>(
                name: "HomeWorkFileId",
                table: "Homeworks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_HomeWorkFileId",
                table: "Homeworks",
                column: "HomeWorkFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_CourseFiles_HomeWorkFileId",
                table: "Homeworks",
                column: "HomeWorkFileId",
                principalTable: "CourseFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
