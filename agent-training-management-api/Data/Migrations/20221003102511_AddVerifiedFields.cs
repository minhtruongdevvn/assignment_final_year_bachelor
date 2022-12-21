using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class AddVerifiedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EQ",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "IQ",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ReactionTime",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SelfDiscipline",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Sex",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Stamina",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Strength",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EQ",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IQ",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ReactionTime",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SelfDiscipline",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5058), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5061) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5070), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5071) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5072), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5072) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5074), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5074) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5075), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5076) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5085), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5085) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5086), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5087) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5088), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5088) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5089), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5092), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5093) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5094), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5094) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5095), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5096) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5097), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5097) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5098), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5099) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5123), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5124) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5128), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5129) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5130), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5131) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5139), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5139) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5141), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5141) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5142), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5142) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5143), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5144) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5145), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5145) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5146), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5147) });

            migrationBuilder.UpdateData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5148), new DateTime(2022, 9, 18, 4, 46, 23, 482, DateTimeKind.Utc).AddTicks(5148) });
        }
    }
}
