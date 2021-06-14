using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment.Migrations
{
    public partial class Add3Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonAns",
                table: "MonAns");

            migrationBuilder.DropColumn(
                name: "FoodID",
                table: "MonAns");

            migrationBuilder.AddColumn<int>(
                name: "MonAnID",
                table: "MonAns",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonAns",
                table: "MonAns",
                column: "MonAnID");

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    KhachHangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 150, nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Describe = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.KhachHangID);
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    DonHangID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangID = table.Column<int>(nullable: false),
                    OrderDay = table.Column<DateTime>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.DonHangID);
                    table.ForeignKey(
                        name: "FK_DonHangs_KhachHangs_KhachHangID",
                        column: x => x.KhachHangID,
                        principalTable: "KhachHangs",
                        principalColumn: "KhachHangID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHangChiTiets",
                columns: table => new
                {
                    ChiTietID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonHangID = table.Column<int>(nullable: false),
                    MonAnID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Note = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangChiTiets", x => x.ChiTietID);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_DonHangs_DonHangID",
                        column: x => x.DonHangID,
                        principalTable: "DonHangs",
                        principalColumn: "DonHangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHangChiTiets_MonAns_MonAnID",
                        column: x => x.MonAnID,
                        principalTable: "MonAns",
                        principalColumn: "MonAnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_DonHangID",
                table: "DonHangChiTiets",
                column: "DonHangID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangChiTiets_MonAnID",
                table: "DonHangChiTiets",
                column: "MonAnID");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_KhachHangID",
                table: "DonHangs",
                column: "KhachHangID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHangChiTiets");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonAns",
                table: "MonAns");

            migrationBuilder.DropColumn(
                name: "MonAnID",
                table: "MonAns");

            migrationBuilder.AddColumn<int>(
                name: "FoodID",
                table: "MonAns",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonAns",
                table: "MonAns",
                column: "FoodID");
        }
    }
}
