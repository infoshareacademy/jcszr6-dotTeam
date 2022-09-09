using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanAndRide.Database.Migrations
{
    public partial class AddGoogleMapsEncodedLineToRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsEncodedLine",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMapsEncodedLine",
                table: "Routes");
        }
    }
}
