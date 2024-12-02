using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerRegisterationFlow.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICNumber = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPhoneVerified = table.Column<bool>(type: "bit", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: true),
                    HasAcceptedTerms = table.Column<bool>(type: "bit", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBiometricLoginEnabled = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "Email", "HasAcceptedTerms", "ICNumber", "IsBiometricLoginEnabled", "IsEmailVerified", "IsPhoneVerified", "LastModifiedBy", "LastModifiedDate", "Name", "PasswordHash", "Phone", "Salt" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2024, 12, 1, 14, 37, 22, 976, DateTimeKind.Local).AddTicks(3838), "email1@email.com", null, 123456, null, null, null, "System", new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2209), "Ahmed 1", "", "0101010101", "" },
                    { 2, "System", new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2926), "email2@email.com", null, 1234567, null, null, null, "System", new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2932), "Ahmed 2", "", "01010101012", "" },
                    { 3, "System", new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2935), "email3@email.com", null, 12345678, null, null, null, "System", new DateTime(2024, 12, 1, 14, 37, 22, 979, DateTimeKind.Local).AddTicks(2937), "Ahmed 3", "", "01010101013", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Phone",
                table: "Customers",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
