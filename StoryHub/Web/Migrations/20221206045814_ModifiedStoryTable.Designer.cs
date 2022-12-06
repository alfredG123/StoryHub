﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Data;

#nullable disable

namespace Web.Migrations
{
    [DbContext(typeof(ProgramDbContext))]
    [Migration("20221206045814_ModifiedStoryTable")]
    partial class ModifiedStoryTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Web.Models.CharacterDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CharacterData");
                });

            modelBuilder.Entity("Web.Models.CharacterReferenceLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReferenceID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("CharacterReferenceLinkItem");
                });

            modelBuilder.Entity("Web.Models.CustomFieldDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CustomFieldData");
                });

            modelBuilder.Entity("Web.Models.PlotCharacterLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlotID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("PlotCharacterLinkItem");
                });

            modelBuilder.Entity("Web.Models.PlotDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DramaType")
                        .HasColumnType("int");

                    b.Property<string>("Goal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlotType")
                        .HasColumnType("int");

                    b.Property<string>("Scene")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PlotData");
                });

            modelBuilder.Entity("Web.Models.PlotPlotLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("PlotID")
                        .HasColumnType("int");

                    b.Property<int>("SubPlotID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("PlotPlotLinkItem");
                });

            modelBuilder.Entity("Web.Models.PlotRegionLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlotID")
                        .HasColumnType("int");

                    b.Property<int>("RegionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("PlotRegionLinkItem");
                });

            modelBuilder.Entity("Web.Models.ReferenceDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelatedURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ReferenceData");
                });

            modelBuilder.Entity("Web.Models.RegionCharacterLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<int>("RegionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("RegionCharacterLinkItem");
                });

            modelBuilder.Entity("Web.Models.RegionDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("RegionData");
                });

            modelBuilder.Entity("Web.Models.RegionReferenceLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReferenceID")
                        .HasColumnType("int");

                    b.Property<int>("RegionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("RegionReferenceLinkItem");
                });

            modelBuilder.Entity("Web.Models.RegionRegionLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("RegionID")
                        .HasColumnType("int");

                    b.Property<int>("SubRegionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("RegionRegionLinkItem");
                });

            modelBuilder.Entity("Web.Models.StoryCharacterLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CharacterID")
                        .HasColumnType("int");

                    b.Property<int>("StoryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("StoryCharacterLinkItem");
                });

            modelBuilder.Entity("Web.Models.StoryCustomFieldLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CustomFieldID")
                        .HasColumnType("int");

                    b.Property<int>("StoryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("StoryCustomFieldLinkItem");
                });

            modelBuilder.Entity("Web.Models.StoryDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("StoryData");
                });

            modelBuilder.Entity("Web.Models.StoryPlotLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("PlotID")
                        .HasColumnType("int");

                    b.Property<int>("StoryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("StoryPlotLinkItem");
                });

            modelBuilder.Entity("Web.Models.StoryReferenceLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("ReferenceID")
                        .HasColumnType("int");

                    b.Property<int>("StoryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("StoryReferenceLinkItem");
                });

            modelBuilder.Entity("Web.Models.StoryRegionLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("RegionID")
                        .HasColumnType("int");

                    b.Property<int>("StoryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("StoryRegionLinkItem");
                });

            modelBuilder.Entity("Web.Models.StoryTimelineLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("StoryID")
                        .HasColumnType("int");

                    b.Property<int>("TimelineID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("StoryTimelineLinkItem");
                });

            modelBuilder.Entity("Web.Models.TimelineDataModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TimelineData");
                });

            modelBuilder.Entity("Web.Models.TimelinePlotLinkItemModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("PlotID")
                        .HasColumnType("int");

                    b.Property<int>("TimelineID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("TimelinePlotLinkItem");
                });
#pragma warning restore 612, 618
        }
    }
}
