using Microsoft.EntityFrameworkCore.Migrations;

namespace MatchingGame2.Migrations
{
    public partial class addgamestatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "View_ActiveGames");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Games");
        }
    }
}
