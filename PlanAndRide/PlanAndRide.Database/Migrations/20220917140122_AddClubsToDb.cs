using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanAndRide.Database.Migrations
{
    public partial class AddClubsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "UserRide",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Routes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Rides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShareRide = table.Column<bool>(type: "bit", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRide_ClubId",
                table: "UserRide",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ClubId",
                table: "Routes",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_ClubId",
                table: "Rides",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClubId",
                table: "Reviews",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ApplicationUserId",
                table: "Clubs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Clubs_ClubId",
                table: "Reviews",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Clubs_ClubId",
                table: "Rides",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Clubs_ClubId",
                table: "Routes",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRide_Clubs_ClubId",
                table: "UserRide",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Clubs_ClubId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Clubs_ClubId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Clubs_ClubId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRide_Clubs_ClubId",
                table: "UserRide");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_UserRide_ClubId",
                table: "UserRide");

            migrationBuilder.DropIndex(
                name: "IX_Routes_ClubId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Rides_ClubId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ClubId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "UserRide");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Reviews");
        }
    }
}
