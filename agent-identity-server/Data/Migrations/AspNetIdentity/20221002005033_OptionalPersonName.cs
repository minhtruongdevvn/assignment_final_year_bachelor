using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentIdentityServer.Data.Migrations.AspNetIdentity
{
	public partial class OptionalPersonName : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "FamilyName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true
			);

			migrationBuilder.AddColumn<string>(
				name: "GivenName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true
			);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(name: "FamilyName", table: "AspNetUsers");

			migrationBuilder.DropColumn(name: "GivenName", table: "AspNetUsers");
		}
	}
}
