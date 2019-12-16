using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _7Element.Migrations
{
    public partial class openToPredsGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Open",
                table: "PredsGame",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-fffffffff123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6acdfe5e-4145-4750-8038-4c3ad3648554", "AQAAAAEAACcQAAAAEN61kl5UgL3hJvC6pWS4EzP0O+JsKa4CvyQ5QZL9r4qr4COfV21Ec29R8J8fI7HTZg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ad7ca0b7-35e8-409f-aefa-7197abe13d42", "AQAAAAEAACcQAAAAEHnEXBXWcA0F0Y9e0j/p7qx8W1Up4i1kS1jiBH2spCdTtmI7ClbvHGV+ViC4UkgsLg==" });

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 14, 18, 26, 688, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 14, 18, 26, 689, DateTimeKind.Local).AddTicks(4586));

            migrationBuilder.UpdateData(
                table: "PredsGame",
                keyColumn: "PredsGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 14, 18, 26, 690, DateTimeKind.Local).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 14, 18, 26, 689, DateTimeKind.Local).AddTicks(6474));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 14, 18, 26, 689, DateTimeKind.Local).AddTicks(7280));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Open",
                table: "PredsGame");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-fffffffff123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e795f27-f7bc-4a68-8440-8f742a31ad15", "AQAAAAEAACcQAAAAEDa/38B4g5mb30izYmuUsGsokS/gebieY015YAscJGstvQijSrDqqUfweGjRS0Orpg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e818902-d9c1-4e65-981c-003c87479cc4", "AQAAAAEAACcQAAAAEBjMxOl1x0QfN4v0JBnFM5Ji6k2tvuLphbT6YIIyvGoJiuv6H06XS2DlW9s8R2MeUA==" });

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 895, DateTimeKind.Local).AddTicks(1778));

            migrationBuilder.UpdateData(
                table: "PickupGame",
                keyColumn: "PickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 895, DateTimeKind.Local).AddTicks(9221));

            migrationBuilder.UpdateData(
                table: "PredsGame",
                keyColumn: "PredsGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 896, DateTimeKind.Local).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 896, DateTimeKind.Local).AddTicks(1155));

            migrationBuilder.UpdateData(
                table: "UserPickupGame",
                keyColumn: "UserPickupGameId",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2019, 12, 16, 13, 13, 20, 896, DateTimeKind.Local).AddTicks(1967));
        }
    }
}
