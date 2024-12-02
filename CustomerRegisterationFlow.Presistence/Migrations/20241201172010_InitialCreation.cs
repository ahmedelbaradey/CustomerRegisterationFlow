using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRegisterationFlow.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsPhoneVerified",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 19, 20, 9, 827, DateTimeKind.Local).AddTicks(7772), new DateTime(2024, 12, 1, 19, 20, 9, 830, DateTimeKind.Local).AddTicks(2103) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 19, 20, 9, 830, DateTimeKind.Local).AddTicks(2993), new DateTime(2024, 12, 1, 19, 20, 9, 830, DateTimeKind.Local).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 19, 20, 9, 830, DateTimeKind.Local).AddTicks(3004), new DateTime(2024, 12, 1, 19, 20, 9, 830, DateTimeKind.Local).AddTicks(3006) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Customers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPhoneVerified",
                table: "Customers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "IsEmailVerified", "IsPhoneVerified", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 14, 37, 22, 976, DateTimeKind.Local).AddTicks(3838), null, null, new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2209) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "IsEmailVerified", "IsPhoneVerified", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2926), null, null, new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2932) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "IsEmailVerified", "IsPhoneVerified", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2935), null, null, new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2937) });
        }
    }
}
