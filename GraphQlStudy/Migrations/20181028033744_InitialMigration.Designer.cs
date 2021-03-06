﻿// <auto-generated />
using GraphQlStudy.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GraphQlStudy.Migrations
{
    [DbContext(typeof(RelationalDbContext))]
    [Migration("20181028033744_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GraphQlStudy.Models.Entities.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ClosingHour");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("OpeningHour");

                    b.HasKey("Id");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("GraphQlStudy.Models.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("Photo")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("GraphQlStudy.Models.Entities.StudentInClass", b =>
                {
                    b.Property<int>("ClassId");

                    b.Property<int>("StudentId");

                    b.Property<double>("JoinedTime");

                    b.HasKey("ClassId", "StudentId");

                    b.HasIndex("ClassId")
                        .IsUnique();

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentInClass");
                });

            modelBuilder.Entity("GraphQlStudy.Models.Entities.StudentInClass", b =>
                {
                    b.HasOne("GraphQlStudy.Models.Entities.Class", "Class")
                        .WithOne("StudentInClass")
                        .HasForeignKey("GraphQlStudy.Models.Entities.StudentInClass", "ClassId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GraphQlStudy.Models.Entities.Student", "Student")
                        .WithOne("StudentInClass")
                        .HasForeignKey("GraphQlStudy.Models.Entities.StudentInClass", "StudentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
