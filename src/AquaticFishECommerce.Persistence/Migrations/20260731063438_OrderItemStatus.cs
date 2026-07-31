using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaticFishECommerce.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutForDeliveryAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PackedAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingAt",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OutForDeliveryAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PackedAt",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ShippingAt",
                table: "OrderItems");
        }
    }
}
