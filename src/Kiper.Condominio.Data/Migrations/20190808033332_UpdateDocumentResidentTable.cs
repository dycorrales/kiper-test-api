using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiper.Condominio.Data.Migrations
{
    public partial class UpdateDocumentResidentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Resident",
                newName: "Document");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 38, DateTimeKind.Local).AddTicks(1514),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(7292));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 38, DateTimeKind.Local).AddTicks(766),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(6029));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 33, 31, 989, DateTimeKind.Local).AddTicks(9468),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 107, DateTimeKind.Local).AddTicks(6927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 33, 31, 979, DateTimeKind.Local).AddTicks(7841),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 100, DateTimeKind.Local).AddTicks(4786));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 23, DateTimeKind.Local).AddTicks(7990),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(6233));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 22, DateTimeKind.Local).AddTicks(3795),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(4964));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Document",
                table: "Resident",
                newName: "Number");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(7292),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 38, DateTimeKind.Local).AddTicks(1514));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Resident",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 150, DateTimeKind.Local).AddTicks(6029),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 38, DateTimeKind.Local).AddTicks(766));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 107, DateTimeKind.Local).AddTicks(6927),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 33, 31, 989, DateTimeKind.Local).AddTicks(9468));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Condominium",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 100, DateTimeKind.Local).AddTicks(4786),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 33, 31, 979, DateTimeKind.Local).AddTicks(7841));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(6233),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 23, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Apartment",
                nullable: false,
                defaultValue: new DateTime(2019, 8, 8, 0, 20, 35, 135, DateTimeKind.Local).AddTicks(4964),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 8, 8, 0, 33, 32, 22, DateTimeKind.Local).AddTicks(3795));
        }
    }
}
