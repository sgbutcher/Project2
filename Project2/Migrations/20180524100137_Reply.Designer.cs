﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Project2.Models;
using System;

namespace Project2.Migrations
{
    [DbContext(typeof(Project2Context))]
    [Migration("20180524100137_Reply")]
    partial class Reply
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project2.Models.Blurb", b =>
                {
                    b.Property<int>("BlurbID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("Date");

                    b.Property<int>("ReplyID");

                    b.Property<string>("TKeyID");

                    b.Property<string>("Title");

                    b.Property<string>("UserID");

                    b.HasKey("BlurbID");

                    b.ToTable("Blurb");
                });
#pragma warning restore 612, 618
        }
    }
}
