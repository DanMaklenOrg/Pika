﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pika.DataLayer;

#nullable disable

namespace Pika.DataLayer.Migrations
{
    [DbContext(typeof(PikaDataContext))]
    [Migration("20230323182612_AddAchievementDbModel")]
    partial class AddAchievementDbModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActionDbModelTagDbModel", b =>
                {
                    b.Property<Guid>("ActionsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("ActionsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("TagAction", (string)null);
                });

            modelBuilder.Entity("EntityDbModelTagDbModel", b =>
                {
                    b.Property<Guid>("EntitiesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.HasKey("EntitiesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("EntityTag", (string)null);
                });

            modelBuilder.Entity("Pika.DataLayer.Model.AchievementDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("DomainId")
                        .HasColumnType("uuid");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Achievement");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ActionDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DomainId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Action");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.DomainDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("RootEntryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RootEntryId");

                    b.ToTable("Domain");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.EntityDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DomainId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Entity");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.EntryDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DomainId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.HasIndex("ParentId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ObjectiveDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int>("RequiredCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ObjectiveTargetDbModel", b =>
                {
                    b.Property<Guid>("ObjectiveId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uuid");

                    b.HasKey("ObjectiveId", "TargetId");

                    b.HasIndex("TargetId");

                    b.ToTable("ObjectiveTargets");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ProgressDbModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ObjectiveId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uuid");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "ObjectiveId", "TargetId");

                    b.HasIndex("TargetId");

                    b.HasIndex("UserId");

                    b.HasIndex("ObjectiveId", "TargetId");

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ProjectDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DomainId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.TagDbModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DomainId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ActionDbModelTagDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.ActionDbModel", null)
                        .WithMany()
                        .HasForeignKey("ActionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pika.DataLayer.Model.TagDbModel", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntityDbModelTagDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.EntityDbModel", null)
                        .WithMany()
                        .HasForeignKey("EntitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pika.DataLayer.Model.TagDbModel", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Pika.DataLayer.Model.AchievementDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.DomainDbModel", "Domain")
                        .WithMany("Achievements")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ActionDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.DomainDbModel", "Domain")
                        .WithMany()
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.DomainDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.EntryDbModel", "RootEntry")
                        .WithMany()
                        .HasForeignKey("RootEntryId");

                    b.Navigation("RootEntry");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.EntityDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.DomainDbModel", "Domain")
                        .WithMany("Entities")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.EntryDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.DomainDbModel", "Domain")
                        .WithMany("RelatedEntries")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pika.DataLayer.Model.EntryDbModel", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Domain");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ObjectiveDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.ProjectDbModel", "Project")
                        .WithMany("Objectives")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ObjectiveTargetDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.ObjectiveDbModel", "Objective")
                        .WithMany()
                        .HasForeignKey("ObjectiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pika.DataLayer.Model.EntryDbModel", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Objective");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ProgressDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.ObjectiveDbModel", "Objective")
                        .WithMany()
                        .HasForeignKey("ObjectiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pika.DataLayer.Model.EntryDbModel", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pika.DataLayer.Model.ObjectiveTargetDbModel", null)
                        .WithMany()
                        .HasForeignKey("ObjectiveId", "TargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Objective");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ProjectDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.DomainDbModel", "Domain")
                        .WithMany("Projects")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.TagDbModel", b =>
                {
                    b.HasOne("Pika.DataLayer.Model.DomainDbModel", "Domain")
                        .WithMany()
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.DomainDbModel", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("Entities");

                    b.Navigation("Projects");

                    b.Navigation("RelatedEntries");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.EntryDbModel", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Pika.DataLayer.Model.ProjectDbModel", b =>
                {
                    b.Navigation("Objectives");
                });
#pragma warning restore 612, 618
        }
    }
}
