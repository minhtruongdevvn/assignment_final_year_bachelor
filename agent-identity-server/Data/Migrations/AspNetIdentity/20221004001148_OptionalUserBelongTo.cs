using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentIdentityServer.Data.Migrations.AspNetIdentity
{
	public partial class OptionalUserBelongTo : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "BelongTo",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true
			);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(name: "BelongTo", table: "AspNetUsers");
		}
	}
}
