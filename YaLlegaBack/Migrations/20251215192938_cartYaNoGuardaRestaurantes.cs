using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YaLlegaBack.Migrations
{
    /// <inheritdoc />
    public partial class cartYaNoGuardaRestaurantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Restaurants_RestaurantId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_RestaurantId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Carts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_RestaurantId",
                table: "Carts",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Restaurants_RestaurantId",
                table: "Carts",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
