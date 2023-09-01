using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s10.Migrations
{
    public partial class AddingIndexEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AppUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_Email",
                table: "AppUser",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppUser_Email",
                table: "AppUser");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AppUser",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
