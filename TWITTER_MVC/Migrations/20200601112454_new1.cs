using Microsoft.EntityFrameworkCore.Migrations;

namespace TWITTER_MVC.Migrations
{
    public partial class new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Users_UserID",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Retweets_Users_UserID",
                table: "Retweets");

            migrationBuilder.DropIndex(
                name: "IX_Retweets_UserID",
                table: "Retweets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserID",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Retweets");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "LikeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Retweets",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "LikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_UserID",
                table: "Retweets",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserID",
                table: "Like",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Users_UserID",
                table: "Like",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Retweets_Users_UserID",
                table: "Retweets",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
