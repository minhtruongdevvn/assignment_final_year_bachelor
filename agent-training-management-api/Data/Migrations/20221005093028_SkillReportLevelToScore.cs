using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class SkillReportLevelToScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Skills_SkillId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "SkillReports");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "SkillReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SkillId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Skills_SkillId",
                table: "Classes",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Skills_SkillId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "SkillReports");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "SkillReports",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SkillId",
                table: "Classes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Skills_SkillId",
                table: "Classes",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id");
        }
    }
}
