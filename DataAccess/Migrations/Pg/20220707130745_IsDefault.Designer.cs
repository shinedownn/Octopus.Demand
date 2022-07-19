﻿// <auto-generated />
using System;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations.Pg
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20220707130745_IsDefault")]
    partial class IsDefault
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.HasSequence("Actions_ActionId_seq", "public");

            modelBuilder.HasSequence("Departments_DepartmentId_seq", "public");

            modelBuilder.HasSequence("HotelDemandActions_HotelDemandActionId_seq", "public");

            modelBuilder.HasSequence("HotelDemandOnRequests_HotelDemandOnRequestId_seq", "public");

            modelBuilder.HasSequence("HotelDemands_HotelDemandId_seq", "public");

            modelBuilder.HasSequence("Logs_Id_seq", "public");

            modelBuilder.HasSequence("MainDemandActions_MainDemandActionId_seq", "public");

            modelBuilder.HasSequence("MainDemands_MainDemandId_seq", "public")
                .StartsAt(900000L);

            modelBuilder.HasSequence("OnRequestApprovements_OnRequestApprovementId_seq", "public");

            modelBuilder.HasSequence("OnRequests_OnRequestId_seq", "public");

            modelBuilder.HasSequence("Reminders_ReminderId_seq", "public");

            modelBuilder.HasSequence("TourDemandActions_TourDemandActionId_seq", "public");

            modelBuilder.HasSequence("TourDemandOnRequests_TourDemandOnRequestId_seq", "public");

            modelBuilder.HasSequence("TourDemands_TourDemandId_seq", "public");

            modelBuilder.Entity("Core.Entities.Concrete.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"Logs_Id_seq\"'::regclass)");

                    b.Property<string>("Exception")
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .HasColumnType("text");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Entities.Concrete.Action", b =>
                {
                    b.Property<int>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"Actions_ActionId_seq\"'::regclass)");

                    b.Property<string>("ActionType")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ActionId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("Entities.Concrete.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"Departments_DepartmentId_seq\"'::regclass)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Entities.Concrete.HotelDemand", b =>
                {
                    b.Property<int>("HotelDemandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"HotelDemands_HotelDemandId_seq\"'::regclass)");

                    b.Property<int>("AdultCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ChildCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("HotelId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int>("MainDemandId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("TotalCount")
                        .HasColumnType("integer");

                    b.HasKey("HotelDemandId");

                    b.HasIndex("MainDemandId");

                    b.ToTable("HotelDemands");
                });

            modelBuilder.Entity("Entities.Concrete.HotelDemandAction", b =>
                {
                    b.Property<int?>("HotelDemandActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"HotelDemandActions_HotelDemandActionId_seq\"'::regclass)");

                    b.Property<int>("ActionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("HotelDemandId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int?>("MainDemandId")
                        .HasColumnType("integer");

                    b.HasKey("HotelDemandActionId");

                    b.ToTable("HotelDemandActions");
                });

            modelBuilder.Entity("Entities.Concrete.HotelDemandOnRequest", b =>
                {
                    b.Property<int?>("HotelDemandOnRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"HotelDemandOnRequests_HotelDemandOnRequestId_seq\"'::regclass)");

                    b.Property<int?>("ApprovalRequestedDepartmentId")
                        .HasColumnType("integer");

                    b.Property<bool?>("Approved")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ApprovingDepartmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("AskingForApprovalDepartmentId")
                        .HasColumnType("integer");

                    b.Property<bool>("ConfirmationRequested")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("HotelDemandId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int?>("MainDemandId")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int>("OnRequestId")
                        .HasColumnType("integer");

                    b.Property<string>("WhoApproves")
                        .HasColumnType("text");

                    b.HasKey("HotelDemandOnRequestId");

                    b.ToTable("HotelDemandOnRequests");
                });

            modelBuilder.Entity("Entities.Concrete.MainDemand", b =>
                {
                    b.Property<int>("MainDemandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"MainDemands_MainDemandId_seq\"'::regclass)");

                    b.Property<string>("AreaCode")
                        .HasColumnType("text");

                    b.Property<int>("ContactId")
                        .HasColumnType("integer");

                    b.Property<string>("CountryCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("DemandChannel")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirmName")
                        .HasColumnType("text");

                    b.Property<string>("FirmTitle")
                        .HasColumnType("text");

                    b.Property<string>("FullPhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFirm")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("RequestCode")
                        .HasColumnType("text");

                    b.Property<string>("ReservationNumber")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("MainDemandId");

                    b.ToTable("MainDemands");
                });

            modelBuilder.Entity("Entities.Concrete.MainDemandAction", b =>
                {
                    b.Property<int>("MainDemandActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"MainDemandActions_MainDemandActionId_seq\"'::regclass)");

                    b.Property<int>("ActionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int>("MainDemandId")
                        .HasColumnType("integer");

                    b.HasKey("MainDemandActionId");

                    b.ToTable("MainDemandActions");
                });

            modelBuilder.Entity("Entities.Concrete.OnRequest", b =>
                {
                    b.Property<int>("OnRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"OnRequests_OnRequestId_seq\"'::regclass)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("OnRequestId");

                    b.ToTable("OnRequests");
                });

            modelBuilder.Entity("Entities.Concrete.OnRequestApprovement", b =>
                {
                    b.Property<int>("OnRequestApprovementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"OnRequestApprovements_OnRequestApprovementId_seq\"'::regclass)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<int>("OnRequestId")
                        .HasColumnType("integer");

                    b.HasKey("OnRequestApprovementId");

                    b.ToTable("OnRequestApprovements");
                });

            modelBuilder.Entity("Entities.Concrete.Reminder", b =>
                {
                    b.Property<int>("ReminderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"Reminders_ReminderId_seq\"'::regclass)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("HotelDemandActionId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ReminderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("TourDemandActionId")
                        .HasColumnType("integer");

                    b.HasKey("ReminderId");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("Entities.Concrete.RequestChannel", b =>
                {
                    b.Property<int>("RequestChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("RequestChannelId");

                    b.ToTable("RequestChannels");
                });

            modelBuilder.Entity("Entities.Concrete.TourDemand", b =>
                {
                    b.Property<int>("TourDemandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"TourDemands_TourDemandId_seq\"'::regclass)");

                    b.Property<int>("AdultCount")
                        .HasColumnType("integer");

                    b.Property<int>("ChildCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int>("MainDemandId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Period")
                        .HasColumnType("text");

                    b.Property<int>("TotalCount")
                        .HasColumnType("integer");

                    b.Property<int>("TourId")
                        .HasColumnType("integer");

                    b.HasKey("TourDemandId");

                    b.HasIndex("MainDemandId");

                    b.ToTable("TourDemands");
                });

            modelBuilder.Entity("Entities.Concrete.TourDemandAction", b =>
                {
                    b.Property<int?>("TourDemandActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"TourDemandActions_TourDemandActionId_seq\"'::regclass)");

                    b.Property<int>("ActionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int>("MainDemandId")
                        .HasColumnType("integer");

                    b.Property<int>("TourDemandId")
                        .HasColumnType("integer");

                    b.HasKey("TourDemandActionId");

                    b.ToTable("TourDemandActions");
                });

            modelBuilder.Entity("Entities.Concrete.TourDemandOnRequest", b =>
                {
                    b.Property<int>("TourDemandOnRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValueSql("nextval('\"TourDemandOnRequests_TourDemandOnRequestId_seq\"'::regclass)");

                    b.Property<int?>("ApprovalRequestedDepartmentId")
                        .HasColumnType("integer");

                    b.Property<bool?>("Approved")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ApprovingDepartmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("AskingForApprovalDepartmentId")
                        .HasColumnType("integer");

                    b.Property<bool>("ConfirmationRequested")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUserName")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<int>("MainDemandId")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int>("OnRequestId")
                        .HasColumnType("integer");

                    b.Property<int>("TourDemandId")
                        .HasColumnType("integer");

                    b.Property<string>("WhoApproves")
                        .HasColumnType("text");

                    b.HasKey("TourDemandOnRequestId");

                    b.ToTable("TourDemandOnRequests");
                });

            modelBuilder.Entity("Entities.Concrete.HotelDemand", b =>
                {
                    b.HasOne("Entities.Concrete.MainDemand", null)
                        .WithMany("HotelDemands")
                        .HasForeignKey("MainDemandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Concrete.TourDemand", b =>
                {
                    b.HasOne("Entities.Concrete.MainDemand", null)
                        .WithMany("TourDemands")
                        .HasForeignKey("MainDemandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Concrete.MainDemand", b =>
                {
                    b.Navigation("HotelDemands");

                    b.Navigation("TourDemands");
                });
#pragma warning restore 612, 618
        }
    }
}
