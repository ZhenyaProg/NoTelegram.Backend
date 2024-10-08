﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NoTelegram.DataAccess.PostgreSQL;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NoTelegram.DataAccess.PostgreSQL.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20240910151901_createChats")]
    partial class createChats
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatsEntityUsersEntity", b =>
                {
                    b.Property<Guid>("ChatsChatId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InterlocutorsSecurityId")
                        .HasColumnType("uuid");

                    b.HasKey("ChatsChatId", "InterlocutorsSecurityId");

                    b.HasIndex("InterlocutorsSecurityId");

                    b.ToTable("ChatsEntityUsersEntity");
                });

            modelBuilder.Entity("NoTelegram.DataAccess.PostgreSQL.Entities.ChatsEntity", b =>
                {
                    b.Property<Guid>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ChatAccess")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("ChatName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("ChatId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("NoTelegram.DataAccess.PostgreSQL.Entities.UsersEntity", b =>
                {
                    b.Property<Guid>("SecurityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AuthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Authenticated")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SecurityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChatsEntityUsersEntity", b =>
                {
                    b.HasOne("NoTelegram.DataAccess.PostgreSQL.Entities.ChatsEntity", null)
                        .WithMany()
                        .HasForeignKey("ChatsChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoTelegram.DataAccess.PostgreSQL.Entities.UsersEntity", null)
                        .WithMany()
                        .HasForeignKey("InterlocutorsSecurityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
