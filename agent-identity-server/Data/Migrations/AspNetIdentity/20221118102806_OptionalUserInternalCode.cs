using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgentIdentityServer.Data.Migrations.AspNetIdentity
{
	public partial class OptionalUserInternalCode : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "InternalCode",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true
			);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(name: "InternalCode", table: "AspNetUsers");
		}
	}
}
