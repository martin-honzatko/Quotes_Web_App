using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuotesWebApp.Migrations
{
    public partial class DatabaseChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteTags",
                table: "QuoteTags");

            migrationBuilder.DropIndex(
                name: "IX_QuoteTags_QuoteId",
                table: "QuoteTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QuoteTags");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteTags",
                table: "QuoteTags",
                columns: new[] { "QuoteId", "TagId" });

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 10, 22, 13, 12, 15, 416, DateTimeKind.Local).AddTicks(5790));

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2020, 10, 22, 13, 12, 15, 419, DateTimeKind.Local).AddTicks(9727));

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2020, 10, 22, 13, 12, 15, 419, DateTimeKind.Local).AddTicks(9915));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuoteTags",
                table: "QuoteTags");

            migrationBuilder.DeleteData(
                table: "QuoteTags",
                keyColumns: new[] { "QuoteId", "TagId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "QuoteTags",
                keyColumns: new[] { "QuoteId", "TagId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "QuoteTags",
                keyColumns: new[] { "QuoteId", "TagId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QuoteTags",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuoteTags",
                table: "QuoteTags",
                column: "Id");

            migrationBuilder.InsertData(
                table: "QuoteTags",
                columns: new[] { "Id", "QuoteId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2020, 10, 22, 12, 28, 4, 668, DateTimeKind.Local).AddTicks(7696));

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2020, 10, 22, 12, 28, 4, 672, DateTimeKind.Local).AddTicks(380));

            migrationBuilder.UpdateData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2020, 10, 22, 12, 28, 4, 672, DateTimeKind.Local).AddTicks(529));

            migrationBuilder.CreateIndex(
                name: "IX_QuoteTags_QuoteId",
                table: "QuoteTags",
                column: "QuoteId");
        }
    }
}
