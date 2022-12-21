using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[]
                {
                    "Id",
                    "CreatedAt",
                    "CreatedBy",
                    "DayOfWeek",
                    "EndAt",
                    "Index",
                    "StartAt",
                    "UpdatedAt",
                    "UpdatedBy"
                },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4556),
                        "system",
                        1,
                        new TimeSpan(0, 10, 0, 0, 0),
                        1,
                        new TimeSpan(0, 8, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4556),
                        "system"
                    },
                    {
                        2,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4574),
                        "system",
                        1,
                        new TimeSpan(0, 12, 0, 0, 0),
                        2,
                        new TimeSpan(0, 10, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4574),
                        "system"
                    },
                    {
                        3,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4575),
                        "system",
                        1,
                        new TimeSpan(0, 15, 0, 0, 0),
                        3,
                        new TimeSpan(0, 13, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4575),
                        "system"
                    },
                    {
                        4,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4577),
                        "system",
                        1,
                        new TimeSpan(0, 17, 0, 0, 0),
                        4,
                        new TimeSpan(0, 15, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4577),
                        "system"
                    },
                    {
                        5,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4579),
                        "system",
                        2,
                        new TimeSpan(0, 10, 0, 0, 0),
                        1,
                        new TimeSpan(0, 8, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4579),
                        "system"
                    },
                    {
                        6,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4582),
                        "system",
                        2,
                        new TimeSpan(0, 12, 0, 0, 0),
                        2,
                        new TimeSpan(0, 10, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4582),
                        "system"
                    },
                    {
                        7,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4583),
                        "system",
                        2,
                        new TimeSpan(0, 15, 0, 0, 0),
                        3,
                        new TimeSpan(0, 13, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4583),
                        "system"
                    },
                    {
                        8,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4584),
                        "system",
                        2,
                        new TimeSpan(0, 17, 0, 0, 0),
                        4,
                        new TimeSpan(0, 15, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4584),
                        "system"
                    },
                    {
                        9,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4586),
                        "system",
                        3,
                        new TimeSpan(0, 10, 0, 0, 0),
                        1,
                        new TimeSpan(0, 8, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4586),
                        "system"
                    },
                    {
                        10,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4588),
                        "system",
                        3,
                        new TimeSpan(0, 12, 0, 0, 0),
                        2,
                        new TimeSpan(0, 10, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4588),
                        "system"
                    },
                    {
                        11,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4590),
                        "system",
                        3,
                        new TimeSpan(0, 15, 0, 0, 0),
                        3,
                        new TimeSpan(0, 13, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4590),
                        "system"
                    },
                    {
                        12,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4599),
                        "system",
                        3,
                        new TimeSpan(0, 17, 0, 0, 0),
                        4,
                        new TimeSpan(0, 15, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4599),
                        "system"
                    },
                    {
                        13,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4600),
                        "system",
                        4,
                        new TimeSpan(0, 10, 0, 0, 0),
                        1,
                        new TimeSpan(0, 8, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4600),
                        "system"
                    },
                    {
                        14,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4602),
                        "system",
                        4,
                        new TimeSpan(0, 12, 0, 0, 0),
                        2,
                        new TimeSpan(0, 10, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4602),
                        "system"
                    },
                    {
                        15,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4603),
                        "system",
                        4,
                        new TimeSpan(0, 15, 0, 0, 0),
                        3,
                        new TimeSpan(0, 13, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4603),
                        "system"
                    },
                    {
                        16,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4605),
                        "system",
                        4,
                        new TimeSpan(0, 17, 0, 0, 0),
                        4,
                        new TimeSpan(0, 15, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4605),
                        "system"
                    },
                    {
                        17,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4606),
                        "system",
                        5,
                        new TimeSpan(0, 10, 0, 0, 0),
                        1,
                        new TimeSpan(0, 8, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4606),
                        "system"
                    },
                    {
                        18,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4608),
                        "system",
                        5,
                        new TimeSpan(0, 12, 0, 0, 0),
                        2,
                        new TimeSpan(0, 10, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4608),
                        "system"
                    },
                    {
                        19,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4610),
                        "system",
                        5,
                        new TimeSpan(0, 15, 0, 0, 0),
                        3,
                        new TimeSpan(0, 13, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4610),
                        "system"
                    },
                    {
                        20,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4611),
                        "system",
                        5,
                        new TimeSpan(0, 17, 0, 0, 0),
                        4,
                        new TimeSpan(0, 15, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4611),
                        "system"
                    },
                    {
                        21,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4613),
                        "system",
                        6,
                        new TimeSpan(0, 10, 0, 0, 0),
                        1,
                        new TimeSpan(0, 8, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4613),
                        "system"
                    },
                    {
                        22,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4614),
                        "system",
                        6,
                        new TimeSpan(0, 12, 0, 0, 0),
                        2,
                        new TimeSpan(0, 10, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4614),
                        "system"
                    },
                    {
                        23,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4615),
                        "system",
                        6,
                        new TimeSpan(0, 15, 0, 0, 0),
                        3,
                        new TimeSpan(0, 13, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4615),
                        "system"
                    },
                    {
                        24,
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4617),
                        "system",
                        6,
                        new TimeSpan(0, 17, 0, 0, 0),
                        4,
                        new TimeSpan(0, 15, 0, 0, 0),
                        new DateTime(2022, 7, 3, 10, 9, 15, 861, DateTimeKind.Utc).AddTicks(4617),
                        "system"
                    }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 1);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 2);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 3);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 4);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 5);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 6);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 7);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 8);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 9);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 10);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 11);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 12);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 13);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 14);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 15);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 16);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 17);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 18);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 19);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 20);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 21);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 22);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 23);

            migrationBuilder.DeleteData(table: "Slots", keyColumn: "Id", keyValue: 24);
        }
    }
}
