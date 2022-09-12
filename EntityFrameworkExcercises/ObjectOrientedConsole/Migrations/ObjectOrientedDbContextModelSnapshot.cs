﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ObjectOrientedConsole.Services;

#nullable disable

namespace ObjectOrientedConsole.Migrations
{
    [DbContext(typeof(ObjectOrientedDbContext))]
    partial class ObjectOrientedDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ObjectOrientedConsole.Models.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("ObjectOrientedConsole.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ApplicantId")
                        .HasColumnType("int");

                    b.Property<string>("JobOfferName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("ObjectOrientedConsole.Models.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("DegreeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassingYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("ObjectOrientedConsole.Models.Application", b =>
                {
                    b.HasOne("ObjectOrientedConsole.Models.Applicant", null)
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantId");
                });

            modelBuilder.Entity("ObjectOrientedConsole.Models.Education", b =>
                {
                    b.HasOne("ObjectOrientedConsole.Models.Application", null)
                        .WithMany("Educations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("ObjectOrientedConsole.Models.Applicant", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("ObjectOrientedConsole.Models.Application", b =>
                {
                    b.Navigation("Educations");
                });
#pragma warning restore 612, 618
        }
    }
}
