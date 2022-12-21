using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class SkillOnlyHasOneCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CategoryId",
                table: "Skills",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Categories_CategoryId",
                table: "Skills",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Categories_CategoryId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CategoryId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "SkillCategories",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategories", x => new { x.SkillId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_SkillCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillCategories_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8176), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8178) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8183), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8183) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8184), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8184) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8185), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8185) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8186), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8186) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8188), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8189) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8189), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8190) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8190), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8191) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8191), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8192) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8193), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8193) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8194), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8194) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8194), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8195) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8195), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8196) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8196), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8197) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8197), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8198) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8198), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8199) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8199), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8200) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8201), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8201) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8202), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8202) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8203), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8203) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8204), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8205), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8205) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8206), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8206) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8206), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc).AddTicks(8207) });

            migrationBuilder.CreateIndex(
                name: "IX_SkillCategories_CategoryId",
                table: "SkillCategories",
                column: "CategoryId");
        }
    }
}
