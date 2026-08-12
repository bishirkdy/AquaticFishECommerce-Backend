using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaticFishECommerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRefundedFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Refunded",
                table: "OrderItems",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Refunded",
                table: "OrderItems");
        }
    }
}
