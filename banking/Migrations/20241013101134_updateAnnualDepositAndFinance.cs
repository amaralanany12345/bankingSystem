using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace banking.Migrations
{
    /// <inheritdoc />
    public partial class updateAnnualDepositAndFinance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualDeposits_accounts_accountId",
                table: "AnnualDeposits");

            migrationBuilder.DropForeignKey(
                name: "FK_finances_accounts_accountId",
                table: "finances");

            migrationBuilder.RenameColumn(
                name: "accountId",
                table: "finances",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_finances_accountId",
                table: "finances",
                newName: "IX_finances_customerId");

            migrationBuilder.RenameColumn(
                name: "accountId",
                table: "AnnualDeposits",
                newName: "customerId");

            migrationBuilder.RenameIndex(
                name: "IX_AnnualDeposits_accountId",
                table: "AnnualDeposits",
                newName: "IX_AnnualDeposits_customerId");

            migrationBuilder.AddColumn<string>(
                name: "FinanceNumber",
                table: "finances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "identityNumber",
                table: "finances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "annualDepositNumber",
                table: "AnnualDeposits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "identityNumber",
                table: "AnnualDeposits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualDeposits_customers_customerId",
                table: "AnnualDeposits",
                column: "customerId",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_finances_customers_customerId",
                table: "finances",
                column: "customerId",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnualDeposits_customers_customerId",
                table: "AnnualDeposits");

            migrationBuilder.DropForeignKey(
                name: "FK_finances_customers_customerId",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "FinanceNumber",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "identityNumber",
                table: "finances");

            migrationBuilder.DropColumn(
                name: "annualDepositNumber",
                table: "AnnualDeposits");

            migrationBuilder.DropColumn(
                name: "identityNumber",
                table: "AnnualDeposits");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "finances",
                newName: "accountId");

            migrationBuilder.RenameIndex(
                name: "IX_finances_customerId",
                table: "finances",
                newName: "IX_finances_accountId");

            migrationBuilder.RenameColumn(
                name: "customerId",
                table: "AnnualDeposits",
                newName: "accountId");

            migrationBuilder.RenameIndex(
                name: "IX_AnnualDeposits_customerId",
                table: "AnnualDeposits",
                newName: "IX_AnnualDeposits_accountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnualDeposits_accounts_accountId",
                table: "AnnualDeposits",
                column: "accountId",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_finances_accounts_accountId",
                table: "finances",
                column: "accountId",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
