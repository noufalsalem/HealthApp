using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class FollowEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Following",
                columns: table => new
                {
                    SourceUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    FollowedUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Following", x => new { x.SourceUserId, x.FollowedUserId });
                    table.ForeignKey(
                        name: "FK_Following_Users_FollowedUserId",
                        column: x => x.FollowedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Following_Users_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Following_FollowedUserId",
                table: "Following",
                column: "FollowedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Following");
        }
    }
}
