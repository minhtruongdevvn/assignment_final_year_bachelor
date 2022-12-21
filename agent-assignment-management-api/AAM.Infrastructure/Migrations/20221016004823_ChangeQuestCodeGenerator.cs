using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAM.Infrastructure.Migrations
{
    public partial class ChangeQuestCodeGenerator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Quests",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "CONCAT(\r\n                            REPLACE(SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), 0, CHARINDEX(' ', CONVERT(VARCHAR,  [DateCreated], 20))), '-', '')\r\n                            ,\r\n                            '.'\r\n                            ,\r\n                            REPLACE(SUBSTRING(CONVERT(VARCHAR,  [DateCreated], 20), CHARINDEX(' ', CONVERT(VARCHAR,  [DateCreated], 20)) + 1,8), ':', '.')\r\n                        )",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComputedColumnSql: "CONCAT(\r\n                            REPLACE(SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), 0, CHARINDEX(' ', CONVERT(VARCHAR, [DateCreated], 20))), '-', '/')\r\n                            ,\r\n                            '/'\r\n                            ,\r\n                            SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), CHARINDEX(' ', CONVERT(VARCHAR, [DateCreated], 20)) + 1, 5)\r\n                        )",
                oldStored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Quests",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "CONCAT(\r\n                            REPLACE(SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), 0, CHARINDEX(' ', CONVERT(VARCHAR, [DateCreated], 20))), '-', '/')\r\n                            ,\r\n                            '/'\r\n                            ,\r\n                            SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), CHARINDEX(' ', CONVERT(VARCHAR, [DateCreated], 20)) + 1, 5)\r\n                        )",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComputedColumnSql: "CONCAT(\r\n                            REPLACE(SUBSTRING(CONVERT(VARCHAR, [DateCreated], 20), 0, CHARINDEX(' ', CONVERT(VARCHAR,  [DateCreated], 20))), '-', '')\r\n                            ,\r\n                            '.'\r\n                            ,\r\n                            REPLACE(SUBSTRING(CONVERT(VARCHAR,  [DateCreated], 20), CHARINDEX(' ', CONVERT(VARCHAR,  [DateCreated], 20)) + 1,8), ':', '.')\r\n                        )",
                oldStored: true);
        }
    }
}
