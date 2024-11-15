﻿// <auto-generated />
using MeuSiteMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace MeuSiteMVC.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20241113173452_CriandoTabelaContatos")]
    partial class CriandoTabelaContatos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeuSiteMVC.Models.ContatoModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CELULAR");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NOME");

                    b.HasKey("id");

                    b.ToTable("CONTATOS");
                });
#pragma warning restore 612, 618
        }
    }
}
