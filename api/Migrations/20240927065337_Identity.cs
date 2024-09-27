using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace billiardchamps.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f4974568-b6c3-4e5e-b410-6f4e96968dc0", "1b526a0b-4739-4c70-9ebe-1c77d1ed237f", "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f4974568-b6c3-4e5e-b410-6f4e96968dc0");
        }
    }
}
