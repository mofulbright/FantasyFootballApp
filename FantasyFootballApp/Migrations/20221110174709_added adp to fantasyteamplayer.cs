using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyFootballApp.Migrations
{
    public partial class addedadptofantasyteamplayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ADP",
                table: "FantasyPlayers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ADP",
                table: "FantasyPlayers");
        }
    }
}
