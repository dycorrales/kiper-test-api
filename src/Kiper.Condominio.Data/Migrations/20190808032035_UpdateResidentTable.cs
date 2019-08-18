using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiper.Condominio.Data.Migrations
{
    public partial class UpdateResidentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(7292),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(9092));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(6029),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(8464));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Resident",
                type: "varchar(25)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 107, DateTimeKind.Local).AddTicks(6927),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 801, DateTimeKind.Local).AddTicks(1902));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 100, DateTimeKind.Local).AddTicks(4786),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 793, DateTimeKind.Local).AddTicks(2160));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(6233),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(8278));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(4964),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(7638));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(9092),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(7292));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 835, DateTimeKind.Local).AddTicks(8464),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(6029));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Resident",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 801, DateTimeKind.Local).AddTicks(1902),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 107, DateTimeKind.Local).AddTicks(6927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 793, DateTimeKind.Local).AddTicks(2160),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 100, DateTimeKind.Local).AddTicks(4786));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(8278),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(6233));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 7, 23, 24, 57, 825, DateTimeKind.Local).AddTicks(7638),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(4964));
        }
    }
}
