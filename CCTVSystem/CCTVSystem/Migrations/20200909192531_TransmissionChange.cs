using Microsoft.EntityFrameworkCore.Migrations;

namespace CCTVSystem.Migrations
{
    public partial class TransmissionChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transmissions_Cameras_CameraId",
                table: "Transmissions");

            migrationBuilder.DropIndex(
                name: "IX_Transmissions_CameraId",
                table: "Transmissions");

            migrationBuilder.AlterColumn<int>(
                name: "CameraId",
                table: "Transmissions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CameraId",
                table: "Transmissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Transmissions_CameraId",
                table: "Transmissions",
                column: "CameraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transmissions_Cameras_CameraId",
                table: "Transmissions",
                column: "CameraId",
                principalTable: "Cameras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
