using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class before_DateParser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 13, 1, 10, 1, 616, DateTimeKind.Local).AddTicks(4774));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 13, 1, 10, 1, 616, DateTimeKind.Local).AddTicks(4811));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 13, 1, 10, 1, 616, DateTimeKind.Local).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 13, 1, 10, 1, 616, DateTimeKind.Local).AddTicks(4817));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 13, 1, 10, 1, 616, DateTimeKind.Local).AddTicks(4819));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 11, 2, 49, 8, 142, DateTimeKind.Local).AddTicks(3992));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 11, 2, 49, 8, 142, DateTimeKind.Local).AddTicks(4055));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 11, 2, 49, 8, 142, DateTimeKind.Local).AddTicks(4058));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 11, 2, 49, 8, 142, DateTimeKind.Local).AddTicks(4062));

            migrationBuilder.UpdateData(
                table: "RssFeeds",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 10, 11, 2, 49, 8, 142, DateTimeKind.Local).AddTicks(4065));
        }
    }
}
