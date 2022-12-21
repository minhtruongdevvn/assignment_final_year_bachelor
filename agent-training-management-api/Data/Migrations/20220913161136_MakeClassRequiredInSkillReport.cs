using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class MakeClassRequiredInSkillReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillReports_Classes_ClassId",
                table: "SkillReports"
            );

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "SkillReports",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                computedColumnSql: "CONCAT(\r\n			'ATS',\r\n			CAST(\r\n				SUBSTRING(\r\n					CONVERT(VARCHAR, [CreatedAt], 111),\r\n					0,\r\n					CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111))\r\n				) AS INTEGER\r\n			) % 1000,\r\n			'.',\r\n			RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n		)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComputedColumnSql: "CONCAT(\r\n                'ATS', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                oldStored: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Operators",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                computedColumnSql: "CONCAT(\r\n			'ATO',\r\n			CAST(\r\n				SUBSTRING(\r\n					CONVERT(VARCHAR, [CreatedAt], 111),\r\n					0,\r\n					CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111))\r\n				) AS INTEGER\r\n			) % 1000,\r\n			'.',\r\n			RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n		)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComputedColumnSql: "CONCAT(\r\n                'ATO', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                oldStored: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Lecturers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                computedColumnSql: "CONCAT(\r\n			'ATL',\r\n			CAST(\r\n				SUBSTRING(\r\n					CONVERT(VARCHAR, [CreatedAt], 111),\r\n					0,\r\n					CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111))\r\n				) AS INTEGER\r\n			) % 1000,\r\n			'.',\r\n			RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n		)",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComputedColumnSql: "CONCAT(\r\n                'ATL', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                oldStored: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_SkillReports_Classes_ClassId",
                table: "SkillReports",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkillReports_Classes_ClassId",
                table: "SkillReports"
            );

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "SkillReports",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                computedColumnSql: "CONCAT(\r\n                'ATS', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComputedColumnSql: "CONCAT(\r\n			'ATS',\r\n			CAST(\r\n				SUBSTRING(\r\n					CONVERT(VARCHAR, [CreatedAt], 111),\r\n					0,\r\n					CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111))\r\n				) AS INTEGER\r\n			) % 1000,\r\n			'.',\r\n			RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n		)",
                oldStored: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Operators",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                computedColumnSql: "CONCAT(\r\n                'ATO', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComputedColumnSql: "CONCAT(\r\n			'ATO',\r\n			CAST(\r\n				SUBSTRING(\r\n					CONVERT(VARCHAR, [CreatedAt], 111),\r\n					0,\r\n					CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111))\r\n				) AS INTEGER\r\n			) % 1000,\r\n			'.',\r\n			RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n		)",
                oldStored: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Lecturers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                computedColumnSql: "CONCAT(\r\n                'ATL', \r\n                CAST(\r\n                    SUBSTRING(\r\n                        CONVERT(VARCHAR, [CreatedAt], 111), \r\n                        0, \r\n                        CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111)\r\n                    )\r\n                ) AS INTEGER) % 1000, \r\n                '.', \r\n                RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n            )",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComputedColumnSql: "CONCAT(\r\n			'ATL',\r\n			CAST(\r\n				SUBSTRING(\r\n					CONVERT(VARCHAR, [CreatedAt], 111),\r\n					0,\r\n					CHARINDEX('/', CONVERT(VARCHAR, [CreatedAt], 111))\r\n				) AS INTEGER\r\n			) % 1000,\r\n			'.',\r\n			RIGHT('000000' + CAST([Id] % 1000000 AS NVARCHAR), 6)\r\n		)",
                oldStored: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_SkillReports_Classes_ClassId",
                table: "SkillReports",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id"
            );
        }
    }
}
