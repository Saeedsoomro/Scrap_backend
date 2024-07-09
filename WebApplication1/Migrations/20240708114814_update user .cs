using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserManagementId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserManagementId",
                table: "Images",
                column: "UserManagementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_UserManagements_UserManagementId",
                table: "Images",
                column: "UserManagementId",
                principalTable: "UserManagements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_UserManagements_UserManagementId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_UserManagementId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "UserManagementId",
                table: "Images");
        }
    }
}
