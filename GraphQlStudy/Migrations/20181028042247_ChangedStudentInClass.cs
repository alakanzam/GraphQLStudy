using Microsoft.EntityFrameworkCore.Migrations;

namespace GraphQlStudy.Migrations
{
    public partial class ChangedStudentInClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentInClass_ClassId",
                table: "StudentInClass");

            migrationBuilder.DropIndex(
                name: "IX_StudentInClass_StudentId",
                table: "StudentInClass");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInClass_StudentId",
                table: "StudentInClass",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentInClass_StudentId",
                table: "StudentInClass");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInClass_ClassId",
                table: "StudentInClass",
                column: "ClassId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInClass_StudentId",
                table: "StudentInClass",
                column: "StudentId",
                unique: true);
        }
    }
}
