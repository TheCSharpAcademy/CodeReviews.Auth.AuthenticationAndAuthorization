using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMvc.Migrations
{
    /// <inheritdoc />
    public partial class ChangePublishDateToIntYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Book");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
