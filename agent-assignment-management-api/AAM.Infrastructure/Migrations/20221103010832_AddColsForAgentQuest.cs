using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAM.Infrastructure.Migrations
{
    public partial class AddColsForAgentQuest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "AgentQuests",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "AgentQuests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "SuccessRate",
                table: "AgentQuests",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "AgentQuests");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "AgentQuests");

            migrationBuilder.DropColumn(
                name: "SuccessRate",
                table: "AgentQuests");
        }
    }
}
