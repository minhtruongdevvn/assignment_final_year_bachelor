using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class AddAgeColumnForStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc), new DateTime(2022, 10, 3, 10, 25, 10, 961, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc) });
        }
    }
}
