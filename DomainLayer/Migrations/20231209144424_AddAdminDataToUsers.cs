using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class AddAdminDataToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "Gender", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ef403e1a-4459-4c93-968a-5a5bb40a4e9f", 0, "6d0c6b40-bb46-4f7a-b8c4-fc02585c7921", new DateTime(2023, 12, 9, 16, 44, 23, 538, DateTimeKind.Local).AddTicks(6974), "admin@gmail.com", false, "Admin", 1, null, "Admin", true, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEGznVOSEVjd8ylReGZGJZA4JP3GHqVfVBXPkTTo3veGiPzXD+/t+SNcrjdNuxPrGPQ==", "01027485927", false, "f42951f0-c40c-475a-844d-6022b92297a0", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ef403e1a-4459-4c93-968a-5a5bb40a4e9f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "ef403e1a-4459-4c93-968a-5a5bb40a4e9f" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ef403e1a-4459-4c93-968a-5a5bb40a4e9f");
        }
    }
}
