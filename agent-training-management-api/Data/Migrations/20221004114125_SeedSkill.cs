using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmAPI.Data.Migrations
{
    public partial class SeedSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "ranged_attack", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 2, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "melee_fight", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 3, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "body_movement", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 4, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "vehicle", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 5, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "mindset", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 6, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "criminal_exploration", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 7, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "personal_neat", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 8, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "basic", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 9, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "human_interaction", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 10, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "technology", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "pistol", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 2, 1, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "rifle", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 3, 1, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "sniper_rifle", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 4, 2, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "taekwondo", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 5, 2, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "karatedo", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 6, 2, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "melee_combat", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 7, 3, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "running", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 8, 3, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "swimming", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 9, 4, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "driving", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 10, 4, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "piloting", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 11, 5, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "thinking_out_of_the_box", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 12, 5, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "critical_thinking", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 13, 5, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "problem_solving", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 14, 5, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "planning", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 15, 5, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "calming", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 16, 5, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "leadership", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 17, 6, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "investigate", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 18, 6, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "judgment", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 19, 6, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "details_inspecting", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 20, 6, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "tracking", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 21, 6, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "tracing", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 22, 7, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "visibility", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 23, 7, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "adaptability", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 24, 7, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "stealing", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 25, 8, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "first_aid", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 26, 9, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "collaboration", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 27, 9, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "communication", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 28, 9, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "interviewing", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 29, 9, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "interrogation", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 30, 9, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "convince", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 31, 10, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "networking", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 32, 10, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "security", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" },
                    { 33, 10, new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system", null, "computing", new DateTime(2022, 10, 4, 10, 44, 37, 637, DateTimeKind.Utc), "system" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
