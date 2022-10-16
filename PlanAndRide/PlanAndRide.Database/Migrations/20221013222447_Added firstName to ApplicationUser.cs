using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanAndRide.Database.Migrations
{
    public partial class AddedfirstNametoApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bikeName",
                table: "AspNetUsers",
                newName: "BikeName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "BikeName",
                table: "AspNetUsers",
                newName: "bikeName");
        }
    }
}
