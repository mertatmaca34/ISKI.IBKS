using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISKI.IBKS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReplacePriorityWithCooldown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumPriorityLevel",
                table: "AlarmUsers");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "AlarmDefinitions",
                newName: "CooldownMinutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CooldownMinutes",
                table: "AlarmDefinitions",
                newName: "Priority");

            migrationBuilder.AddColumn<int>(
                name: "MinimumPriorityLevel",
                table: "AlarmUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
