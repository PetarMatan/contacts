﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using manageContacts.Repositories;

namespace manageContacts.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20190414200440_FillDb")]
    partial class FillDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("manageContacts.Entities.Models.ContactTag", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("tag")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("id");

                    b.HasAlternateKey("tag");

                    b.ToTable("contactTag");
                });

            modelBuilder.Entity("manageContacts.Entities.Models.MobileNumber", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("contactid");

                    b.Property<Guid>("guid");

                    b.Property<string>("number")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("id");

                    b.HasAlternateKey("number");

                    b.HasIndex("contactid");

                    b.ToTable("mobileNumber");
                });

            modelBuilder.Entity("manageContacts.Entities.Models.PhoneContactTags", b =>
                {
                    b.Property<int>("phoneContactId");

                    b.Property<int>("tagId");

                    b.HasKey("phoneContactId", "tagId");

                    b.HasIndex("tagId");

                    b.ToTable("phoneContactTags");
                });

            modelBuilder.Entity("manageContacts.Entities.Models.SimContact", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("guid");

                    b.Property<string>("name");

                    b.Property<string>("surname");

                    b.HasKey("id");

                    b.ToTable("simContacts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("SimContact");
                });

            modelBuilder.Entity("manageContacts.Entities.Models.PhoneContact", b =>
                {
                    b.HasBaseType("manageContacts.Entities.Models.SimContact");

                    b.Property<string>("address");

                    b.Property<string>("email");

                    b.HasDiscriminator().HasValue("PhoneContact");
                });

            modelBuilder.Entity("manageContacts.Entities.Models.MobileNumber", b =>
                {
                    b.HasOne("manageContacts.Entities.Models.SimContact", "contact")
                        .WithMany("MobileNumbers")
                        .HasForeignKey("contactid");
                });

            modelBuilder.Entity("manageContacts.Entities.Models.PhoneContactTags", b =>
                {
                    b.HasOne("manageContacts.Entities.Models.PhoneContact", "phoneContact")
                        .WithMany("contactTags")
                        .HasForeignKey("phoneContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("manageContacts.Entities.Models.ContactTag", "contactTag")
                        .WithMany("PhoneContacts")
                        .HasForeignKey("tagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
