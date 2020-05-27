﻿// <auto-generated />
using System;
using ClashRoyaleDataModel.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClashRoyaleDataModel.Migrations
{
    [DbContext(typeof(ClanParticipationContext))]
    partial class ClanParticipationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("ClashRoyaleDataModel.Models.DonationRecord", b =>
                {
                    b.Property<DateTime>("StoredDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerTag")
                        .HasColumnType("TEXT");

                    b.Property<int>("Donations")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DonationsReceived")
                        .HasColumnType("INTEGER");

                    b.HasKey("StoredDate", "PlayerTag");

                    b.HasIndex("PlayerTag");

                    b.ToTable("DonationRecords");
                });

            modelBuilder.Entity("ClashRoyaleDataModel.Models.Player", b =>
                {
                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Tag");

                    b.ToTable("ClanMembers");
                });

            modelBuilder.Entity("ClashRoyaleDataModel.Models.WarLog", b =>
                {
                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("CreatedDate");

                    b.ToTable("WarHistory");
                });

            modelBuilder.Entity("ClashRoyaleDataModel.Models.WarParticipation", b =>
                {
                    b.Property<string>("PlayerTag")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("WarLogCreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("BattlesPlayed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CardsEarned")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CollectionDayBattlesPlayed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfBattles")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Wins")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayerTag", "WarLogCreatedDate");

                    b.HasIndex("WarLogCreatedDate");

                    b.ToTable("WarParticipations");
                });

            modelBuilder.Entity("ClashRoyaleDataModel.Models.DonationRecord", b =>
                {
                    b.HasOne("ClashRoyaleDataModel.Models.Player", "Player")
                        .WithMany("DonationRecords")
                        .HasForeignKey("PlayerTag")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClashRoyaleDataModel.Models.WarParticipation", b =>
                {
                    b.HasOne("ClashRoyaleDataModel.Models.Player", "Player")
                        .WithMany("WarParticipations")
                        .HasForeignKey("PlayerTag")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClashRoyaleDataModel.Models.WarLog", "Warlog")
                        .WithMany("Participants")
                        .HasForeignKey("WarLogCreatedDate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
