using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TWITTER_MVC.Migrations
{
    public partial class pp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    ProfilePicture = table.Column<string>(nullable: true),
                    BackgroundImg = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Follower",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follower_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    LikeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    TweetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_Like_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Retweets",
                columns: table => new
                {
                    RetweetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullanıcıId = table.Column<int>(nullable: false),
                    TweetID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retweets", x => x.RetweetId);
                    table.ForeignKey(
                        name: "FK_Retweets_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tweets",
                columns: table => new
                {
                    TweetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tweet = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UserName = table.Column<string>(nullable: true),
                    UsName = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    Retweets = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweets", x => x.TweetID);
                    table.ForeignKey(
                        name: "FK_Tweets_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Follower_UserID",
                table: "Follower",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserID",
                table: "Like",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_UserID",
                table: "Retweets",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_UserID",
                table: "Tweets",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Follower");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Retweets");

            migrationBuilder.DropTable(
                name: "Tweets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
