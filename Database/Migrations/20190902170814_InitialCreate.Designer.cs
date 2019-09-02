﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(WebScrapperContext))]
    [Migration("20190902170814_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.InnerLink", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url");

                    b.Property<int?>("WebTargetId");

                    b.Property<string>("XPath");

                    b.HasKey("Id");

                    b.HasIndex("WebTargetId");

                    b.ToTable("InnerLinks");
                });

            modelBuilder.Entity("Models.Ticket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateLastModified");

                    b.Property<DateTime>("EventDate");

                    b.Property<string>("EventName");

                    b.Property<long?>("InnerLinkId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Location");

                    b.Property<decimal?>("Price");

                    b.Property<string>("SeatNumber");

                    b.Property<string>("Section");

                    b.Property<string>("Status");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("InnerLinkId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Models.WebTarget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("EventName");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("WebTargets");
                });

            modelBuilder.Entity("Models.InnerLink", b =>
                {
                    b.HasOne("Models.WebTarget", "ParentLink")
                        .WithMany("InnerLinks")
                        .HasForeignKey("WebTargetId");
                });

            modelBuilder.Entity("Models.Ticket", b =>
                {
                    b.HasOne("Models.InnerLink", "Referrer")
                        .WithMany()
                        .HasForeignKey("InnerLinkId");
                });
#pragma warning restore 612, 618
        }
    }
}