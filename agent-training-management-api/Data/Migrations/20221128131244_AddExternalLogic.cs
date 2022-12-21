using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class AddExternalLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentifyNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExternalInstitution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillIds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalInstitution", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalInstitutionStudent",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ExternalInstitutionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalInstitutionStudent", x => new { x.StudentId, x.ExternalInstitutionId });
                    table.ForeignKey(
                        name: "FK_ExternalInstitutionStudent_ExternalInstitution_ExternalInstitutionId",
                        column: x => x.ExternalInstitutionId,
                        principalTable: "ExternalInstitution",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExternalInstitutionStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalInstitutionStudent_ExternalInstitutionId",
                table: "ExternalInstitutionStudent",
                column: "ExternalInstitutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalInstitutionStudent");

            migrationBuilder.DropTable(
                name: "ExternalInstitution");

            migrationBuilder.DropColumn(
                name: "IdentifyNumber",
                table: "Students");
        }
    }
}
