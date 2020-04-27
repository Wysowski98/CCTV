using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CCTVSystem.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cctvs_Clients_ClientId",
                table: "Cctvs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Cctvs_ClientId",
                table: "Cctvs");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Cctvs");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cctvs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastViewedStream",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Cctvs_UserId",
                table: "Cctvs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cctvs_AspNetUsers_UserId",
                table: "Cctvs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cctvs_AspNetUsers_UserId",
                table: "Cctvs");

            migrationBuilder.DropIndex(
                name: "IX_Cctvs_UserId",
                table: "Cctvs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cctvs");

            migrationBuilder.DropColumn(
                name: "LastViewedStream",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Cctvs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cctvs_ClientId",
                table: "Cctvs",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cctvs_Clients_ClientId",
                table: "Cctvs",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
