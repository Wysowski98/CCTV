using Microsoft.EntityFrameworkCore.Migrations;

namespace CCTVSystem.Migrations
{
    public partial class DeleteVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReadyToDelete",
                table: "Transmissions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadyToDelete",
                table: "Transmissions");
        }
    }
}
