using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace s10.Migrations
{
    public partial class AddedIsAnonymousAndLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "Queja",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Queja",
                type: "geography",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "Queja");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Queja");
        }
    }
}
