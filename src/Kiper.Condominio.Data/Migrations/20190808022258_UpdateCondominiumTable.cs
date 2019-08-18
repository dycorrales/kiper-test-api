using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiper.Condominio.Data.Migrations
{
    public partial class UpdateCondominiumTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(5269),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 475, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(4034),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 475, DateTimeKind.Local).AddTicks(418));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 299, DateTimeKind.Local).AddTicks(6742),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 440, DateTimeKind.Local).AddTicks(5548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 289, DateTimeKind.Local).AddTicks(4869),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 433, DateTimeKind.Local).AddTicks(1344));

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Condominium",
                type: "varchar(9)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(5753),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 465, DateTimeKind.Local).AddTicks(2453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(4275),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 465, DateTimeKind.Local).AddTicks(1357));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 475, DateTimeKind.Local).AddTicks(920),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(5269));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 475, DateTimeKind.Local).AddTicks(418),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(4034));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 440, DateTimeKind.Local).AddTicks(5548),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 299, DateTimeKind.Local).AddTicks(6742));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 433, DateTimeKind.Local).AddTicks(1344),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 289, DateTimeKind.Local).AddTicks(4869));

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Condominium",
                type: "varchar(8)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 465, DateTimeKind.Local).AddTicks(2453),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(5753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 465, DateTimeKind.Local).AddTicks(1357),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(4275));
        }
    }
}
