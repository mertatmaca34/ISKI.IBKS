using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISKI.IBKS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedSensorsJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SelectedSensorsJson",
                table: "StationSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlarmUserSubscriptions_AlarmUserId",
                table: "AlarmUserSubscriptions",
                column: "AlarmUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlarmUserSubscriptions_AlarmUsers_AlarmUserId",
                table: "AlarmUserSubscriptions",
                column: "AlarmUserId",
                principalTable: "AlarmUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlarmUserSubscriptions_AlarmUsers_AlarmUserId",
                table: "AlarmUserSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_AlarmUserSubscriptions_AlarmUserId",
                table: "AlarmUserSubscriptions");

            migrationBuilder.DropColumn(
                name: "SelectedSensorsJson",
                table: "StationSettings");
        }
    }
}
