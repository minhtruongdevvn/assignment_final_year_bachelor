using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class RemoveIndexColOfSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Index", table: "Slots");

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5724),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5724)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5733),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5733)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5735),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5735)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5736),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5736)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5737),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5737)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5740),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5740)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5741),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5741)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5742),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5742)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5743),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5743)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5788),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5788)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5789),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5789)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5790),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5790)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5792),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5792)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5793),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5793)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5794),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5794)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5795),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5795)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5796),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5796)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5798),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5798)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5799),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5799)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5800),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5800)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5802),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5802)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5803),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5803)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5804),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5804)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5805),
                    new DateTime(2022, 7, 11, 22, 32, 43, 562, DateTimeKind.Utc).AddTicks(5805)
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4556),
                    1,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4556)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4574),
                    2,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4574)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4575),
                    3,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4575)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4577),
                    4,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4577)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4579),
                    1,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4579)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4582),
                    2,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4582)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4583),
                    3,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4583)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4584),
                    4,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4584)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4586),
                    1,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4586)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4588),
                    2,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4588)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4590),
                    3,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4590)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4599),
                    4,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4599)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4600),
                    1,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4600)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4602),
                    2,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4602)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4603),
                    3,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4603)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4605),
                    4,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4605)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4606),
                    1,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4606)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4608),
                    2,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4608)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4610),
                    3,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4610)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4611),
                    4,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4611)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4613),
                    1,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4613)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4614),
                    2,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4614)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4615),
                    3,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4615)
                }
            );

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "Index", "UpdatedAt" },
                values: new object[]
                {
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4617),
                    4,
                    new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4617)
                }
            );
        }
    }
}
