using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAM.Infrastructure.Migrations
{
    public partial class AddAgeForAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Agents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Agents");
        }
    }
}
