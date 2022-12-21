using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAM.Infrastructure.Migrations
{
    public partial class ChangeQuestSucessRateToResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuccessRate",
                table: "AgentQuests");

            migrationBuilder.AddColumn<bool>(
                name: "Result",
                table: "AgentQuests",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "AgentQuests");

            migrationBuilder.AddColumn<double>(
                name: "SuccessRate",
                table: "AgentQuests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
