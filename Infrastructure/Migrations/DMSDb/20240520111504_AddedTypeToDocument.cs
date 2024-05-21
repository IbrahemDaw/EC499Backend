using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.DMSDb
{
    /// <inheritdoc />
    public partial class AddedTypeToDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 20, 13, 15, 2, 157, DateTimeKind.Local).AddTicks(7503), new DateTime(2024, 5, 20, 13, 15, 2, 157, DateTimeKind.Local).AddTicks(7515) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 5, 11, 10, 12, 4, 684, DateTimeKind.Local).AddTicks(1566), new DateTime(2024, 5, 11, 10, 12, 4, 684, DateTimeKind.Local).AddTicks(1576) });
        }
    }
}
