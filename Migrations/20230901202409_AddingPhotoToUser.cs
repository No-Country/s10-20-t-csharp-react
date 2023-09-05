using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s10.Migrations
{
    public partial class AddingPhotoToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicAddress",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicAddress",
                table: "AppUser");
        }
    }
}
