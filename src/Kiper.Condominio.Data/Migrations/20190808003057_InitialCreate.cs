using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kiper.Condominio.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Condominium",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 433, DateTimeKind.Local).AddTicks(1344)),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 440, DateTimeKind.Local).AddTicks(5548)),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Street = table.Column<string>(type: "varchar(150)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Complement = table.Column<string>(type: "varchar(150)", nullable: true),
                    Neighbourhood = table.Column<string>(type: "varchar(150)", nullable: false),
                    City = table.Column<string>(type: "varchar(150)", nullable: false),
                    State = table.Column<string>(type: "varchar(150)", nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condominium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 465, DateTimeKind.Local).AddTicks(1357)),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 465, DateTimeKind.Local).AddTicks(2453)),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<string>(type: "varchar(50)", nullable: false),
                    Roof = table.Column<int>(type: "int", nullable: false),
                    CondominiumId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_Condominium_CondominiumId",
                        column: x => x.CondominiumId,
                        principalTable: "Condominium",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resident",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 475, DateTimeKind.Local).AddTicks(418)),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 8, 7, 21, 30, 56, 475, DateTimeKind.Local).AddTicks(920)),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Number = table.Column<string>(type: "varchar(11)", nullable: false),
                    ApartmentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resident", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resident_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_CondominiumId",
                table: "Apartment",
                column: "CondominiumId");

            migrationBuilder.CreateIndex(
                name: "IX_Resident_ApartmentId",
                table: "Resident",
                column: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resident");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "Condominium");
        }
    }
}
