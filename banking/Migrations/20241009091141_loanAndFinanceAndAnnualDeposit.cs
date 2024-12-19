using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace banking.Migrations
{
    /// <inheritdoc />
    public partial class loanAndFinanceAndAnnualDeposit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnualDeposits",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountId = table.Column<int>(type: "int", nullable: false),
                    AnnualDepositValue = table.Column<int>(type: "int", nullable: false),
                    AnnualDepositRepaymentValue = table.Column<int>(type: "int", nullable: false),
                    AnnualDepositRequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnnualDepositPeriod = table.Column<int>(type: "int", nullable: false),
                    AnnualDepositRepaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    acceptAnnualDeposit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualDeposits", x => x.id);
                    table.ForeignKey(
                        name: "FK_AnnualDeposits_accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "finances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountId = table.Column<int>(type: "int", nullable: false),
                    FinanceValue = table.Column<int>(type: "int", nullable: false),
                    FinanceRepaymentValue = table.Column<int>(type: "int", nullable: false),
                    FinanceRequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinancePeriod = table.Column<int>(type: "int", nullable: false),
                    FinanceRepaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    acceptFinance = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_finances", x => x.id);
                    table.ForeignKey(
                        name: "FK_finances_accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountId = table.Column<int>(type: "int", nullable: false),
                    LoanValue = table.Column<int>(type: "int", nullable: false),
                    LoanRepaymentValue = table.Column<int>(type: "int", nullable: false),
                    LoanRequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    loanPeriod = table.Column<int>(type: "int", nullable: false),
                    LoanRepaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    acceptLoan = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loans", x => x.id);
                    table.ForeignKey(
                        name: "FK_loans_accounts_accountId",
                        column: x => x.accountId,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualDeposits_accountId",
                table: "AnnualDeposits",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_finances_accountId",
                table: "finances",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_loans_accountId",
                table: "loans",
                column: "accountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualDeposits");

            migrationBuilder.DropTable(
                name: "finances");

            migrationBuilder.DropTable(
                name: "loans");
        }
    }
}
