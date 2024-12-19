using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace banking.Migrations
{
    /// <inheritdoc />
    public partial class updateAnnualDepositCashing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFinanceRepaid",
                table: "finances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "annualDepositCashing",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    annualDepositId = table.Column<int>(type: "int", nullable: false),
                    annualDepositCashingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    annualDepositCashingValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_annualDepositCashing", x => x.id);
                    table.ForeignKey(
                        name: "FK_annualDepositCashing_AnnualDeposits_annualDepositId",
                        column: x => x.annualDepositId,
                        principalTable: "AnnualDeposits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sentAccountId = table.Column<int>(type: "int", nullable: false),
                    receivedAccountId = table.Column<int>(type: "int", nullable: false),
                    transferValue = table.Column<int>(type: "int", nullable: false),
                    transferTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfers", x => x.id);
                    table.ForeignKey(
                        name: "FK_transfers_accounts_receivedAccountId",
                        column: x => x.receivedAccountId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_transfers_accounts_sentAccountId",
                        column: x => x.sentAccountId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_annualDepositCashing_annualDepositId",
                table: "annualDepositCashing",
                column: "annualDepositId");

            migrationBuilder.CreateIndex(
                name: "IX_transfers_receivedAccountId",
                table: "transfers",
                column: "receivedAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transfers_sentAccountId",
                table: "transfers",
                column: "sentAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "annualDepositCashing");

            migrationBuilder.DropTable(
                name: "transfers");

            migrationBuilder.DropColumn(
                name: "isFinanceRepaid",
                table: "finances");
        }
    }
}
