using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Service.Migrations
{
    /// <inheritdoc />
    public partial class AddEventosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventos",
                columns: table => new
                {
                    eventoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entradasDisponibles = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventos", x => x.eventoID);
                });

            migrationBuilder.CreateTable(
                name: "reservas",
                columns: table => new
                {
                    reservaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuarioID = table.Column<int>(type: "int", nullable: false),
                    eventoID = table.Column<int>(type: "int", nullable: false),
                    cantidadEntradas = table.Column<int>(type: "int", nullable: false),
                    fechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estaCancelada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservas", x => x.reservaID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventos");

            migrationBuilder.DropTable(
                name: "reservas");
        }
    }
}
