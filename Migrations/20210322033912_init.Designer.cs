﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SS_API.Data;

namespace SS_API.Migrations
{
    [DbContext(typeof(StreamerContext))]
    [Migration("20210322033912_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("SS_API.Model.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SS_API.Model.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<string>("Image");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ProjectStatus");

                    b.Property<string>("What");

                    b.Property<string>("WhatWillWeDo");

                    b.Property<string>("Why");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SS_API.Model.Project", b =>
                {
                    b.HasOne("SS_API.Model.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
