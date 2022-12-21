using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAM.Infrastructure.Migrations
{
    public partial class AddAutoGenCodeForQuest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Quests",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "CONCAT(\r\n                            REPLACE(SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), 0, CHARINDEX(' ', CONVERT(VARCHAR, [DateCreated], 20))), '-', '/')\r\n                            ,\r\n                            '/'\r\n                            ,\r\n                            SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), CHARINDEX(' ', CONVERT(VARCHAR, [DateCreated], 20)) + 1, 5)\r\n                        )",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quests_Code",
                table: "Quests",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quests_Code",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Quests");
        }
    }
}
