using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressTableneww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Address",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "floorUnit",
                table: "Address",
                newName: "FloorUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Address",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "FloorUnit",
                table: "Address",
                newName: "floorUnit");
        }
    }
}
