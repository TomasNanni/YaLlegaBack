using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YaLlegaBack.Migrations
{
    /// <inheritdoc />
    public partial class segundaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Restaurants_RestaurantId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RestaurantId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Restaurants",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LogoImage",
                table: "Restaurants",
                newName: "UrlLogoImage");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Restaurants",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "ClosingTime", "Contact", "Name", "OpeningTime", "UrlBannerImage", "UrlLogoImage" },
                values: new object[] { 2, new TimeOnly(22, 0, 0), "+54", "mcdonald", new TimeOnly(9, 0, 0), "string", "string" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RestaurantId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Restaurants",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UrlLogoImage",
                table: "Restaurants",
                newName: "LogoImage");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Restaurants",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Restaurants",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RestaurantId",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RestaurantId",
                table: "Users",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Restaurants_RestaurantId",
                table: "Users",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
