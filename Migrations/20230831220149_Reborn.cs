using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s10.Migrations
{
    public partial class Reborn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Category_ID);
                });

            migrationBuilder.CreateTable(
                name: "Locality",
                columns: table => new
                {
                    Locality_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locality", x => x.Locality_ID);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    District_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locality_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.District_ID);
                    table.ForeignKey(
                        name: "FK_District_Locality_Locality_ID",
                        column: x => x.Locality_ID,
                        principalTable: "Locality",
                        principalColumn: "Locality_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Queja",
                columns: table => new
                {
                    Complaint_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District_ID = table.Column<int>(type: "int", nullable: false),
                    Category_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queja", x => x.Complaint_ID);
                    table.ForeignKey(
                        name: "FK_Queja_AppUser_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AppUser",
                        principalColumn: "User_ID");
                    table.ForeignKey(
                        name: "FK_Queja_Category_Category_ID",
                        column: x => x.Category_ID,
                        principalTable: "Category",
                        principalColumn: "Category_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Queja_District_District_ID",
                        column: x => x.District_ID,
                        principalTable: "District",
                        principalColumn: "District_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Comment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Complaint_ID = table.Column<int>(type: "int", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Comment_ID);
                    table.ForeignKey(
                        name: "FK_Comment_AppUser_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AppUser",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Queja_Complaint_ID",
                        column: x => x.Complaint_ID,
                        principalTable: "Queja",
                        principalColumn: "Complaint_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Complaint_ID",
                table: "Comment",
                column: "Complaint_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_User_ID",
                table: "Comment",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_District_Locality_ID",
                table: "District",
                column: "Locality_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Queja_Category_ID",
                table: "Queja",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Queja_District_ID",
                table: "Queja",
                column: "District_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Queja_User_ID",
                table: "Queja",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Queja");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Locality");
        }
    }
}
