using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_Design.Migrations
{
    public partial class seed_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13f817b9-6c9d-41cb-a570-7f639fa8a2a1", "b189abeb-a8c4-4257-8ccd-3f71354ef7c6", "UserManager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4d02929c-d0e9-4cc0-b04c-99dc8cb591ae", "243389a5-4b67-4762-a530-725812eb21f3", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd533534-de0b-49d1-bcd6-7b48727c3076", "c07ec5d3-38d7-4b0a-95a2-2b2c89937ced", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13f817b9-6c9d-41cb-a570-7f639fa8a2a1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d02929c-d0e9-4cc0-b04c-99dc8cb591ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd533534-de0b-49d1-bcd6-7b48727c3076");
        }
    }
}
