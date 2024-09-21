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
                name: "Articles",
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
                    UrlToImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustScore = table.Column<double>(type: "float", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RssSources",
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
                    table.PrimaryKey("PK_RssSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlToImageConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RssSourceId = table.Column<int>(type: "int", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlToImageConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlToImageConfig_RssSources_RssSourceId",
                        column: x => x.RssSourceId,
                        principalTable: "RssSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RssSources",
                columns: new[] { "Id", "Author", "CreatedOn", "Description", "FeedUrl", "Link", "ModifiedOn", "PubDate", "Source", "SourceUrl", "Title" },
                values: new object[,]
                {
                    { 1, "author", new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(2985), "description", "https://mia.mk/feed", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "MIA", "https://mia.mk", "title" },
                    { 2, "dc:creator", new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3049), "content:encoded", "https://telma.com.mk/feed/", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "Telma", "https://telma.com.mk", "title" },
                    { 3, "", new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3052), "content", "https://admin.24.mk/api/rss.xml", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "24Vesti", "https://24.mk", "title" },
                    { 4, "dc:creator", new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3056), "description", "https://sitel.com.mk/rss.xml", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "Sitel", "https://sitel.com.mk", "title" },
                    { 5, "author", new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3059), "content", "https://kanal5.com.mk/rss.aspx", "link", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pubDate", "Kanal5", "https://kanal5.com.mk", "title" }
                });

            migrationBuilder.InsertData(
                table: "UrlToImageConfig",
                columns: new[] { "Id", "Attribute", "CreatedOn", "ModifiedOn", "Query", "Regex", "RssSourceId" },
                values: new object[,]
                {
                    { 1, "url", new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3143), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "enclosure", null, 1 },
                    { 2, null, new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3151), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "content:encoded", "<img[^>]*src=\\\"([^\\\"]*)\\\"", 2 },
                    { 3, "src", new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3154), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "img", null, 3 },
                    { 4, null, new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3158), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "description", "<img[^>]*src=\\\"([^\\\"]*)\\\"", 4 },
                    { 5, null, new DateTime(2024, 9, 21, 23, 25, 11, 564, DateTimeKind.Local).AddTicks(3161), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thumbnail", null, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlToImageConfig_RssSourceId",
                table: "UrlToImageConfig",
                column: "RssSourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "UrlToImageConfig");

            migrationBuilder.DropTable(
                name: "RssSources");
        }
    }
}
