﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240908104911_AddValueColumnToWarningMigration")]
    partial class AddValueColumnToWarningMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.DataReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PatientMeasureId")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PatientMeasureId");

                    b.ToTable("DataReadings");
                });

            modelBuilder.Entity("Domain.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Domain.Entities.PatientMeasure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DeviceId")
                        .HasColumnType("integer");

                    b.Property<string>("Embg")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("MaxThreshold")
                        .HasColumnType("double precision");

                    b.Property<string>("MeasureType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("MinThreshold")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("PatientMeasures");
                });

            modelBuilder.Entity("Domain.Entities.Warning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("CurrentMaxThreshold")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentMinThreshold")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PatientMeasureId")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PatientMeasureId");

                    b.ToTable("Warnings");
                });

            modelBuilder.Entity("Domain.Entities.DataReading", b =>
                {
                    b.HasOne("Domain.Entities.PatientMeasure", "PatientMeasure")
                        .WithMany("DataReadings")
                        .HasForeignKey("PatientMeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientMeasure");
                });

            modelBuilder.Entity("Domain.Entities.PatientMeasure", b =>
                {
                    b.HasOne("Domain.Entities.Device", "Device")
                        .WithMany("PatientMeasures")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("Domain.Entities.Warning", b =>
                {
                    b.HasOne("Domain.Entities.PatientMeasure", "PatientMeasure")
                        .WithMany("Warnings")
                        .HasForeignKey("PatientMeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientMeasure");
                });

            modelBuilder.Entity("Domain.Entities.Device", b =>
                {
                    b.Navigation("PatientMeasures");
                });

            modelBuilder.Entity("Domain.Entities.PatientMeasure", b =>
                {
                    b.Navigation("DataReadings");

                    b.Navigation("Warnings");
                });
#pragma warning restore 612, 618
        }
    }
}
