using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class AddUniqueConstraintOnExternalName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalInstitutionStudent_ExternalInstitution_ExternalInstitutionId",
                table: "ExternalInstitutionStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalInstitutionStudent_Students_StudentId",
                table: "ExternalInstitutionStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalInstitutionStudent",
                table: "ExternalInstitutionStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalInstitution",
                table: "ExternalInstitution");

            migrationBuilder.RenameTable(
                name: "ExternalInstitutionStudent",
                newName: "ExternalInstitutionStudents");

            migrationBuilder.RenameTable(
                name: "ExternalInstitution",
                newName: "ExternalInstitutions");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalInstitutionStudent_ExternalInstitutionId",
                table: "ExternalInstitutionStudents",
                newName: "IX_ExternalInstitutionStudents_ExternalInstitutionId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExternalInstitutions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalInstitutionStudents",
                table: "ExternalInstitutionStudents",
                columns: new[] { "StudentId", "ExternalInstitutionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalInstitutions",
                table: "ExternalInstitutions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalInstitutions_Name",
                table: "ExternalInstitutions",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalInstitutionStudents_ExternalInstitutions_ExternalInstitutionId",
                table: "ExternalInstitutionStudents",
                column: "ExternalInstitutionId",
                principalTable: "ExternalInstitutions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalInstitutionStudents_Students_StudentId",
                table: "ExternalInstitutionStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalInstitutionStudents_ExternalInstitutions_ExternalInstitutionId",
                table: "ExternalInstitutionStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalInstitutionStudents_Students_StudentId",
                table: "ExternalInstitutionStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalInstitutionStudents",
                table: "ExternalInstitutionStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalInstitutions",
                table: "ExternalInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_ExternalInstitutions_Name",
                table: "ExternalInstitutions");

            migrationBuilder.RenameTable(
                name: "ExternalInstitutionStudents",
                newName: "ExternalInstitutionStudent");

            migrationBuilder.RenameTable(
                name: "ExternalInstitutions",
                newName: "ExternalInstitution");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalInstitutionStudents_ExternalInstitutionId",
                table: "ExternalInstitutionStudent",
                newName: "IX_ExternalInstitutionStudent_ExternalInstitutionId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExternalInstitution",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalInstitutionStudent",
                table: "ExternalInstitutionStudent",
                columns: new[] { "StudentId", "ExternalInstitutionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalInstitution",
                table: "ExternalInstitution",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalInstitutionStudent_ExternalInstitution_ExternalInstitutionId",
                table: "ExternalInstitutionStudent",
                column: "ExternalInstitutionId",
                principalTable: "ExternalInstitution",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalInstitutionStudent_Students_StudentId",
                table: "ExternalInstitutionStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
