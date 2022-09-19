using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pichincha.Infrastructure.Migrations
{
    public partial class InitialPostgre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Contrasena = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Genero = table.Column<string>(type: "text", nullable: true),
                    Edad = table.Column<int>(type: "integer", nullable: true),
                    Identificacion = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroCuenta = table.Column<string>(type: "text", nullable: false),
                    TipoCuenta = table.Column<string>(type: "text", nullable: false),
                    Saldo = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuenta_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCuenta = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "text", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Saldo = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimiento_Cuenta_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Cuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdCliente",
                table: "Cuenta",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_IdCuenta",
                table: "Movimiento",
                column: "IdCuenta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
