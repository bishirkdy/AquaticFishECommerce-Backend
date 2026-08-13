using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaticFishECommerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedItemProfit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Profit",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profit",
                table: "OrderItems");
        }
    }
}
