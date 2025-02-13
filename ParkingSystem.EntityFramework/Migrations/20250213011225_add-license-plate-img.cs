using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSystem.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addlicenseplateimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicensePlateImg",
                table: "Vehicles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensePlateImg",
                table: "Vehicles");
        }
    }
}
