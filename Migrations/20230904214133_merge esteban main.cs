using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s10.Migrations
{
    public partial class mergeestebanmain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AppUser");
        }
    }
}
