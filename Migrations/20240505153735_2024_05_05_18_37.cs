using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityTuitionPaymentV2.Migrations
{
    /// <inheritdoc />
    public partial class _2024_05_05_18_37 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    TCKimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTransfer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ReceiverCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    TransferAmount = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTransfer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    EndYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tuition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TuitionTotal = table.Column<double>(type: "float", nullable: false),
                    StudentNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    LastPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TermId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tuition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tuition_Term_TermId",
                        column: x => x.TermId,
                        principalTable: "Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    UniversityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CurrentTuitionFee = table.Column<double>(type: "float", nullable: false),
                    CurrentTermId = table.Column<int>(type: "int", nullable: false),
                    BankAccountId = table.Column<int>(type: "int", nullable: true),
                    TermId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.Id);
                    table.ForeignKey(
                        name: "FK_University_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_University_Term_TermId",
                        column: x => x.TermId,
                        principalTable: "Term",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKimlikNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CurrentUniversityId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_University_CurrentUniversityId",
                        column: x => x.CurrentUniversityId,
                        principalTable: "University",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_CurrentUniversityId",
                table: "Student",
                column: "CurrentUniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuition_TermId",
                table: "Tuition",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_University_BankAccountId",
                table: "University",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_University_TermId",
                table: "University",
                column: "TermId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountTransfer");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Tuition");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "Term");
        }
    }
}
