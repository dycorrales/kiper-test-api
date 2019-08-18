using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiper.Condominio.Data.Migrations
{
    public partial class UpdateZipCodeCondominiumTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(9092),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(5269));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(8464),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(4034));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 801, DateTimeKind.Local).AddTicks(1902),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 299, DateTimeKind.Local).AddTicks(6742));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 793, DateTimeKind.Local).AddTicks(2160),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 289, DateTimeKind.Local).AddTicks(4869));

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Condominium",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(8278),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(5753));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(7638),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(4275));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(5269),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(9092));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 342, DateTimeKind.Local).AddTicks(4034),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(8464));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 299, DateTimeKind.Local).AddTicks(6742),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 801, DateTimeKind.Local).AddTicks(1902));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 289, DateTimeKind.Local).AddTicks(4869),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 793, DateTimeKind.Local).AddTicks(2160));

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Condominium",
                type: "varchar(9)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(5753),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(8278));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 22, 58, 328, DateTimeKind.Local).AddTicks(4275),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(7638));
        }
    }
}
