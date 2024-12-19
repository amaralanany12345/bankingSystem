using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace banking.Migrations
{
    /// <inheritdoc />
    public partial class updateAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "loans");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "accounts",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "accounts");

            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountId = table.Column<int>(type: "int", nullable: false),
                    LoanRepaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanRepaymentValue = table.Column<int>(type: "int", nullable: false),
                    LoanRequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoanValue = table.Column<int>(type: "int", nullable: false),
                    acceptLoan = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    loanPeriod = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_loans_accountId",
                table: "loans",
                column: "accountId");
        }
    }
}
