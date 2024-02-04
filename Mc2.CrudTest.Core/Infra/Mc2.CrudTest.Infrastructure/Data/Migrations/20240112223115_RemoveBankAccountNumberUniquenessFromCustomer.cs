using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBankAccountNumberUniquenessFromCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_BankAccountNumber",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankAccountNumber",
                table: "Customers",
                column: "BankAccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_BankAccountNumber",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BankAccountNumber",
                table: "Customers",
                column: "BankAccountNumber",
                unique: true);
        }
    }
}
