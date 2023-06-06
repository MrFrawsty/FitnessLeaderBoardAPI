using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessLeaderBoardAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserModelsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Activities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserModelId",
                table: "Activities",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserModelId",
                table: "Activities",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_UserModelId",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Activities_UserModelId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Activities");
        }
    }
}
