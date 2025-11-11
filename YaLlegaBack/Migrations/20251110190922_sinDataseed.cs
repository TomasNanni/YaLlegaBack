using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YaLlegaBack.Migrations
{
    /// <inheritdoc />
    public partial class sinDataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "OpenDays",
                table: "Restaurants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Restaurants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_UserId",
                table: "Restaurants",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Users_UserId",
                table: "Restaurants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Users_UserId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_UserId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OpenDays",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "ClosingTime", "Contact", "Name", "OpeningTime", "UrlBannerImage", "UrlLogoImage" },
                values: new object[] { 2, new TimeOnly(22, 0, 0), "+54", "mcdonald", new TimeOnly(9, 0, 0), "string", "string" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailAdress", "FirstName", "LastName", "Password", "RestaurantId" },
                values: new object[] { 1, "tomas@gmail.com", "tomas", "nanni", "contraseña", 2 });
        }
    }
}
