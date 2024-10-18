using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RssFeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PubDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PubDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RssFeedId = table.Column<int>(type: "int", nullable: false),
                    UrlToImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrlToImageConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RssFeedId = table.Column<int>(type: "int", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlToImageConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlToImageConfigs_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RssFeeds",
                columns: new[] { "Id", "Author", "CreatedOn", "Description", "FeedUrl", "Link", "ModifiedOn", "PubDate", "Source", "SourceUrl", "Title" },
                values: new object[,]
                {
                    { 1, "author", new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8581), "description", "https://mia.mk/feed", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "MIA", "https://mia.mk", "title" },
                    { 2, "dc:creator", new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8635), "content:encoded", "https://telma.com.mk/feed/", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "Telma", "https://telma.com.mk", "title" },
                    { 3, "", new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8640), "content", "https://admin.24.mk/api/rss.xml", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "24Vesti", "https://24.mk", "title" },
                    { 4, "dc:creator", new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8644), "description", "https://sitel.com.mk/rss.xml", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "Sitel", "https://sitel.com.mk", "title" },
                    { 5, "author", new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8648), "content", "https://kanal5.com.mk/rss.aspx", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "Kanal5", "https://kanal5.com.mk", "title" }
                });

            migrationBuilder.InsertData(
                table: "UrlToImageConfigs",
                columns: new[] { "Id", "Attribute", "CreatedOn", "ModifiedOn", "Query", "Regex", "RssFeedId" },
                values: new object[,]
                {
                    { 1, "url", new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8685), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "enclosure", null, 1 },
                    { 2, null, new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8693), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "content:encoded", "<img[^>]*src=\\\"([^\\\"]*)\\\"", 2 },
                    { 3, "src", new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8697), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "img", null, 3 },
                    { 4, null, new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8700), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "description", "<img[^>]*src=\\\"([^\\\"]*)\\\"", 4 },
                    { 5, null, new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8703), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thumbnail", null, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FeedUrl",
                table: "Articles",
                column: "FeedUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_RssFeedId",
                table: "Articles",
                column: "RssFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlToImageConfigs_RssFeedId",
                table: "UrlToImageConfigs",
                column: "RssFeedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "UrlToImageConfigs");

            migrationBuilder.DropTable(
                name: "RssFeeds");
        }
    }
}
