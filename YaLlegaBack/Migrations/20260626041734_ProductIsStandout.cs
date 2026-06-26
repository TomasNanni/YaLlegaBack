using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YaLlegaBack.Migrations
{
    /// <inheritdoc />
    public partial class ProductIsStandout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStandout",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStandout",
                table: "Products");
        }
    }
}
