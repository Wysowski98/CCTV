using Microsoft.EntityFrameworkCore.Migrations;

namespace CCTVSystem.Migrations
{
    public partial class NewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Cameras",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_ClientId",
                table: "Cameras",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_AspNetUsers_ClientId",
                table: "Cameras",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_AspNetUsers_ClientId",
                table: "Cameras");

            migrationBuilder.DropIndex(
                name: "IX_Cameras_ClientId",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Cameras");
        }
    }
}
