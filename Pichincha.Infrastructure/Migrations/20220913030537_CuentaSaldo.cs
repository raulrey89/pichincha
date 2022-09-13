using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pichincha.Infrastructure.Migrations
{
    public partial class CuentaSaldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaldoInicial",
                table: "Cuenta",
                newName: "Saldo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Saldo",
                table: "Cuenta",
                newName: "SaldoInicial");
        }
    }
}
