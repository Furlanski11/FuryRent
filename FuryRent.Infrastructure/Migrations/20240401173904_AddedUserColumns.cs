using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuryRent.Infrastructure.Migrations
{
    public partial class AddedUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b38b4548-0a62-4c8d-8d63-1c1b23ccae88", "Georgi", "Peev", "AQAAAAEAACcQAAAAEFigbdj+USGYc08oDzokS+fpACYdvoVbxOU1t4jUzGSdYv+ali/K0vt3KiicIV1cYQ==", "aabd29f9-c394-4ff0-a115-9f5e2b2b846b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a05c49cf-9185-4e42-9bab-acc99214f85d", "Bruce", "Wayne", "userOne@mail.com", "AQAAAAEAACcQAAAAEIY6olbg/hGHy2y5j3q0EIOt8o38GJUW1YcjyJZp7Tk4k/5z0Ahp5JXRVlH3qPfYpw==", "c5a836bb-e421-4e5f-9363-b78b3c30bb44" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a14a984c-ed62-42c6-9a9e-bb91e11411a9", "AQAAAAEAACcQAAAAEC2j/xQzhN4up2KEenE9VnWrPtxFZxWZrl/P08BwQIkC+wvRJc+J7C3ebGpi0TdIOw==", "b0457f4d-919f-4dec-9004-f55882f5f634" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70d1692d-1690-410d-addc-479044847ab0", "agent@mail.com", "AQAAAAEAACcQAAAAENtjOukVsai/45Iw/ExquRqcyWLR7QrBGI2SFafKB+uCbByXvCi+8R3EdVb0MEfJNw==", "e19d6e6b-b64e-41c8-9a0f-0ae890934101" });
        }
    }
}
