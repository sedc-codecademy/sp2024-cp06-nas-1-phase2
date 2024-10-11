using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v2combined_RssFeed_and_UrlToImageConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlToImageConfigs");

            migrationBuilder.DropIndex(
                name: "IX_Articles_FeedUrl",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Attribute",
                table: "RssFeeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Query",
                table: "RssFeeds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Regex",
                table: "RssFeeds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FeedUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Attribute", "CreatedOn", "Query" },
                values: new object[] { "url", new DateTime(2024, 10, 10, 20, 30, 22, 705, DateTimeKind.Local).AddTicks(6611), "enclosure" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "Query", "Regex" },
                values: new object[] { new DateTime(2024, 10, 10, 20, 30, 22, 705, DateTimeKind.Local).AddTicks(6670), "content:encoded", "<img[^>]*src=\\\"([^\\\"]*)\\\"" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Attribute", "CreatedOn", "Query" },
                values: new object[] { "src", new DateTime(2024, 10, 10, 20, 30, 22, 705, DateTimeKind.Local).AddTicks(6675), "img" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "Query", "Regex" },
                values: new object[] { new DateTime(2024, 10, 10, 20, 30, 22, 705, DateTimeKind.Local).AddTicks(6681), "description", "<img[^>]*src=\\\"([^\\\"]*)\\\"" });

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedOn", "Query" },
                values: new object[] { new DateTime(2024, 10, 10, 20, 30, 22, 705, DateTimeKind.Local).AddTicks(6755), "thumbnail" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attribute",
                table: "RssFeeds");

            migrationBuilder.DropColumn(
                name: "Query",
                table: "RssFeeds");

            migrationBuilder.DropColumn(
                name: "Regex",
                table: "RssFeeds");

            migrationBuilder.AlterColumn<string>(
                name: "FeedUrl",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "UrlToImageConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RssFeedId = table.Column<int>(type: "int", nullable: false),
                    Attribute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regex = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8581));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8635));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8640));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8644));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 4, 19, 15, 18, 50, DateTimeKind.Local).AddTicks(8648));

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
                name: "IX_UrlToImageConfigs_RssFeedId",
                table: "UrlToImageConfigs",
                column: "RssFeedId");
        }
    }
}
