using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations.Pg
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateSequence(
                name: "Actions_ActionId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "Departments_DepartmentId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "HotelDemandActions_HotelDemandActionId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "HotelDemandOnRequests_HotelDemandOnRequestId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "HotelDemands_HotelDemandId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "Logs_Id_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "MainDemandActions_MainDemandActionId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "MainDemands_MainDemandId_seq",
                schema: "public",
                startValue: 900000L);

            migrationBuilder.CreateSequence(
                name: "OnRequestApprovements_OnRequestApprovementId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "OnRequests_OnRequestId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "Reminders_ReminderId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "TourDemandActions_TourDemandActionId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "TourDemandOnRequests_TourDemandOnRequestId_seq",
                schema: "public");

            migrationBuilder.CreateSequence(
                name: "TourDemands_TourDemandId_seq",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"Actions_ActionId_seq\"'::regclass)"),
                    ActionType = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ActionId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"Departments_DepartmentId_seq\"'::regclass)"),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "HotelDemandActions",
                columns: table => new
                {
                    HotelDemandActionId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"HotelDemandActions_HotelDemandActionId_seq\"'::regclass)"),
                    MainDemandId = table.Column<int>(type: "integer", nullable: true),
                    HotelDemandId = table.Column<int>(type: "integer", nullable: true),
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDemandActions", x => x.HotelDemandActionId);
                });

            migrationBuilder.CreateTable(
                name: "HotelDemandOnRequests",
                columns: table => new
                {
                    HotelDemandOnRequestId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"HotelDemandOnRequests_HotelDemandOnRequestId_seq\"'::regclass)"),
                    MainDemandId = table.Column<int>(type: "integer", nullable: true),
                    HotelDemandId = table.Column<int>(type: "integer", nullable: true),
                    OnRequestId = table.Column<int>(type: "integer", nullable: false),
                    AskingForApprovalDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    ConfirmationRequested = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovalRequestedDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    Approved = table.Column<bool>(type: "boolean", nullable: true),
                    ApprovingDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    WhoApproves = table.Column<string>(type: "text", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDemandOnRequests", x => x.HotelDemandOnRequestId);
                });

            migrationBuilder.CreateTable(
                name: "HotelDemands",
                columns: table => new
                {
                    HotelDemandId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"HotelDemands_HotelDemandId_seq\"'::regclass)"),
                    MainDemandId = table.Column<int>(type: "integer", nullable: false),
                    HotelId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AdultCount = table.Column<int>(type: "integer", nullable: false),
                    ChildCount = table.Column<int>(type: "integer", nullable: false),
                    TotalCount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelDemands", x => x.HotelDemandId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"Logs_Id_seq\"'::regclass)"),
                    MessageTemplate = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Exception = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainDemandActions",
                columns: table => new
                {
                    MainDemandActionId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"MainDemandActions_MainDemandActionId_seq\"'::regclass)"),
                    MainDemandId = table.Column<int>(type: "integer", nullable: false),
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainDemandActions", x => x.MainDemandActionId);
                });

            migrationBuilder.CreateTable(
                name: "MainDemands",
                columns: table => new
                {
                    MainDemandId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"MainDemands_MainDemandId_seq\"'::regclass)"),
                    RequestCode = table.Column<string>(type: "text", nullable: true),
                    ContactId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    AreaCode = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    FullPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DemandChannel = table.Column<string>(type: "text", nullable: true),
                    ReservationNumber = table.Column<string>(type: "text", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsFirm = table.Column<bool>(type: "boolean", nullable: false),
                    FirmName = table.Column<string>(type: "text", nullable: true),
                    FirmTitle = table.Column<string>(type: "text", nullable: true),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainDemands", x => x.MainDemandId);
                });

            migrationBuilder.CreateTable(
                name: "OnRequestApprovements",
                columns: table => new
                {
                    OnRequestApprovementId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OnRequestApprovements_OnRequestApprovementId_seq\"'::regclass)"),
                    OnRequestId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnRequestApprovements", x => x.OnRequestApprovementId);
                });

            migrationBuilder.CreateTable(
                name: "OnRequests",
                columns: table => new
                {
                    OnRequestId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OnRequests_OnRequestId_seq\"'::regclass)"),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnRequests", x => x.OnRequestId);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    ReminderId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"Reminders_ReminderId_seq\"'::regclass)"),
                    HotelDemandActionId = table.Column<int>(type: "integer", nullable: true),
                    TourDemandActionId = table.Column<int>(type: "integer", nullable: true),
                    ReminderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.ReminderId);
                });

            migrationBuilder.CreateTable(
                name: "TourDemandActions",
                columns: table => new
                {
                    TourDemandActionId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"TourDemandActions_TourDemandActionId_seq\"'::regclass)"),
                    MainDemandId = table.Column<int>(type: "integer", nullable: false),
                    TourDemandId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ActionId = table.Column<int>(type: "integer", nullable: false),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDemandActions", x => x.TourDemandActionId);
                });

            migrationBuilder.CreateTable(
                name: "TourDemandOnRequests",
                columns: table => new
                {
                    TourDemandOnRequestId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"TourDemandOnRequests_TourDemandOnRequestId_seq\"'::regclass)"),
                    MainDemandId = table.Column<int>(type: "integer", nullable: false),
                    TourDemandId = table.Column<int>(type: "integer", nullable: false),
                    OnRequestId = table.Column<int>(type: "integer", nullable: false),
                    AskingForApprovalDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    ConfirmationRequested = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovalRequestedDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    Approved = table.Column<bool>(type: "boolean", nullable: true),
                    ApprovingDepartmentId = table.Column<int>(type: "integer", nullable: true),
                    WhoApproves = table.Column<string>(type: "text", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDemandOnRequests", x => x.TourDemandOnRequestId);
                });

            migrationBuilder.CreateTable(
                name: "TourDemands",
                columns: table => new
                {
                    TourDemandId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"TourDemands_TourDemandId_seq\"'::regclass)"),
                    MainDemandId = table.Column<int>(type: "integer", nullable: false),
                    TourId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Period = table.Column<string>(type: "text", nullable: true),
                    AdultCount = table.Column<int>(type: "integer", nullable: false),
                    ChildCount = table.Column<int>(type: "integer", nullable: false),
                    TotalCount = table.Column<int>(type: "integer", nullable: false),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDemands", x => x.TourDemandId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "HotelDemandActions");

            migrationBuilder.DropTable(
                name: "HotelDemandOnRequests");

            migrationBuilder.DropTable(
                name: "HotelDemands");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MainDemandActions");

            migrationBuilder.DropTable(
                name: "MainDemands");

            migrationBuilder.DropTable(
                name: "OnRequestApprovements");

            migrationBuilder.DropTable(
                name: "OnRequests");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "TourDemandActions");

            migrationBuilder.DropTable(
                name: "TourDemandOnRequests");

            migrationBuilder.DropTable(
                name: "TourDemands");

            migrationBuilder.DropSequence(
                name: "Actions_ActionId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "Departments_DepartmentId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "HotelDemandActions_HotelDemandActionId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "HotelDemandOnRequests_HotelDemandOnRequestId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "HotelDemands_HotelDemandId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "Logs_Id_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "MainDemandActions_MainDemandActionId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "MainDemands_MainDemandId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "OnRequestApprovements_OnRequestApprovementId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "OnRequests_OnRequestId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "Reminders_ReminderId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "TourDemandActions_TourDemandActionId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "TourDemandOnRequests_TourDemandOnRequestId_seq",
                schema: "public");

            migrationBuilder.DropSequence(
                name: "TourDemands_TourDemandId_seq",
                schema: "public");
        }
    }
}
