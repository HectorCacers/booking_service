﻿// <auto-generated />
using System;
using Booking_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Booking_Service.Migrations
{
    [DbContext(typeof(BookingDbContext))]
    [Migration("20240909200205_AddEventosTable")]
    partial class AddEventosTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Booking_Service.Models.Evento", b =>
                {
                    b.Property<int>("EventoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("eventoID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventoID"));

                    b.Property<int>("EntradasDisponibles")
                        .HasColumnType("int")
                        .HasColumnName("entradasDisponibles");

                    b.Property<string>("NombreEvento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nombreEvento");

                    b.HasKey("EventoID");

                    b.ToTable("eventos", (string)null);
                });

            modelBuilder.Entity("Booking_Service.Models.Reserva", b =>
                {
                    b.Property<int>("ReservaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reservaID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservaID"));

                    b.Property<int>("CantidadEntradas")
                        .HasColumnType("int")
                        .HasColumnName("cantidadEntradas");

                    b.Property<bool>("EstaCancelada")
                        .HasColumnType("bit")
                        .HasColumnName("estaCancelada");

                    b.Property<int>("EventoID")
                        .HasColumnType("int")
                        .HasColumnName("eventoID");

                    b.Property<DateTime>("FechaReserva")
                        .HasColumnType("datetime2")
                        .HasColumnName("fechaReserva");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int")
                        .HasColumnName("usuarioID");

                    b.HasKey("ReservaID");

                    b.ToTable("reservas", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
