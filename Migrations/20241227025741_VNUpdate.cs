using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAppPrj.Migrations
{
    /// <inheritdoc />
    public partial class VNUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    MaKhoa = table.Column<string>(type: "TEXT", nullable: false),
                    TenKhoa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.MaKhoa);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    MaSV = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenSV = table.Column<string>(type: "TEXT", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "TEXT", nullable: true),
                    GioiTinh = table.Column<int>(type: "INTEGER", nullable: true),
                    MaKhoa = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.MaSV);
                    table.ForeignKey(
                        name: "FK_Student_Departments_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Departments",
                        principalColumn: "MaKhoa");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_MaKhoa",
                table: "Student",
                column: "MaKhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
