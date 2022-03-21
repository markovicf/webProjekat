﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace BolnicaProjekat.Migrations
{
    [DbContext(typeof(BolnicaContext))]
    [Migration("20220302154058_v5")]
    partial class v5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Bolnica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Bolnice");
                });

            modelBuilder.Entity("Models.Doktor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("OdeljenjeID")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.HasIndex("OdeljenjeID");

                    b.ToTable("Doktori");
                });

            modelBuilder.Entity("Models.Odeljenje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BolnicaID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Specijalizacija")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.HasIndex("BolnicaID");

                    b.ToTable("Odeljenja");
                });

            modelBuilder.Entity("Models.Pacijent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Pacijenti");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoktorID")
                        .HasColumnType("int");

                    b.Property<int?>("PacijentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DoktorID");

                    b.HasIndex("PacijentID");

                    b.ToTable("Spojevi");
                });

            modelBuilder.Entity("Models.Doktor", b =>
                {
                    b.HasOne("Models.Odeljenje", "Odeljenje")
                        .WithMany("Doktori")
                        .HasForeignKey("OdeljenjeID");

                    b.Navigation("Odeljenje");
                });

            modelBuilder.Entity("Models.Odeljenje", b =>
                {
                    b.HasOne("Models.Bolnica", "Bolnica")
                        .WithMany("Odeljenja")
                        .HasForeignKey("BolnicaID");

                    b.Navigation("Bolnica");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.HasOne("Models.Doktor", "Doktor")
                        .WithMany("Spojevi")
                        .HasForeignKey("DoktorID");

                    b.HasOne("Models.Pacijent", "Pacijent")
                        .WithMany("Spojevi")
                        .HasForeignKey("PacijentID");

                    b.Navigation("Doktor");

                    b.Navigation("Pacijent");
                });

            modelBuilder.Entity("Models.Bolnica", b =>
                {
                    b.Navigation("Odeljenja");
                });

            modelBuilder.Entity("Models.Doktor", b =>
                {
                    b.Navigation("Spojevi");
                });

            modelBuilder.Entity("Models.Odeljenje", b =>
                {
                    b.Navigation("Doktori");
                });

            modelBuilder.Entity("Models.Pacijent", b =>
                {
                    b.Navigation("Spojevi");
                });
#pragma warning restore 612, 618
        }
    }
}
