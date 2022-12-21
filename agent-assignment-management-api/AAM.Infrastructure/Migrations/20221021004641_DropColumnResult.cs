using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAM.Infrastructure.Migrations
{
    public partial class DropColumnResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "AgentQuests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Result",
                table: "AgentQuests",
                type: "bit",
                nullable: true);
        }
    }
}
