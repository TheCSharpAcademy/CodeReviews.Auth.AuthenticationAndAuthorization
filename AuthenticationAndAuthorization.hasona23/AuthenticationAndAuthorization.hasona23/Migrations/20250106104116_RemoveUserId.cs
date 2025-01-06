using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationAndAuthorization.hasona23.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SleepRecords_AspNetUsers_UserId",
                table: "SleepRecords");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SleepRecords",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SleepRecords_AspNetUsers_UserId",
                table: "SleepRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SleepRecords_AspNetUsers_UserId",
                table: "SleepRecords");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SleepRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SleepRecords_AspNetUsers_UserId",
                table: "SleepRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
