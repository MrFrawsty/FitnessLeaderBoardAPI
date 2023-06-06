using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessLeaderBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserWithActivityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Activities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Activities");
        }
    }
}
