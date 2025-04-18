﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ToDoApp.repository.dbcontext;

#nullable disable

namespace ToDoApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250415145748_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ToDoApp.models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<DateTime?>("FinishDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("FinishDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("StartDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ToDoApp.models.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("ToDoApp.models.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Description");

                    b.Property<DateTime?>("FinishDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("FinishDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("StartDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("ToDoApp.models.TodoTechnology", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<int>("TechnologyId")
                        .HasColumnType("integer");

                    b.HasKey("ItemId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("TodoTechnologies");
                });

            modelBuilder.Entity("ToDoApp.models.TodoItem", b =>
                {
                    b.HasOne("ToDoApp.models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ToDoApp.models.TodoTechnology", b =>
                {
                    b.HasOne("ToDoApp.models.TodoItem", "Item")
                        .WithMany("Technologies")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoApp.models.Technology", "Technology")
                        .WithMany("TodoTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("ToDoApp.models.Technology", b =>
                {
                    b.Navigation("TodoTechnologies");
                });

            modelBuilder.Entity("ToDoApp.models.TodoItem", b =>
                {
                    b.Navigation("Technologies");
                });
#pragma warning restore 612, 618
        }
    }
}
