using Microsoft.EntityFrameworkCore.Migrations;

namespace EfTeams.Data.Migrations
{
    public partial class _2ndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamLeagues",
                table: "TeamLeagues");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeamLeagues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamLeagues",
                table: "TeamLeagues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLeagues_TeamId",
                table: "TeamLeagues",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamLeagues",
                table: "TeamLeagues");

            migrationBuilder.DropIndex(
                name: "IX_TeamLeagues_TeamId",
                table: "TeamLeagues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamLeagues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamLeagues",
                table: "TeamLeagues",
                columns: new[] { "TeamId", "LeagueId" });
        }
    }
}
