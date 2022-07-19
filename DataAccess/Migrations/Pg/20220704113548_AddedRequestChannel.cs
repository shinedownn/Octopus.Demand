using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations.Pg
{
    public partial class AddedRequestChannel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestChannels",
                columns: table => new
                {
                    RequestChannelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestChannels", x => x.RequestChannelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourDemands_MainDemandId",
                table: "TourDemands",
                column: "MainDemandId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelDemands_MainDemandId",
                table: "HotelDemands",
                column: "MainDemandId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelDemands_MainDemands_MainDemandId",
                table: "HotelDemands",
                column: "MainDemandId",
                principalTable: "MainDemands",
                principalColumn: "MainDemandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourDemands_MainDemands_MainDemandId",
                table: "TourDemands",
                column: "MainDemandId",
                principalTable: "MainDemands",
                principalColumn: "MainDemandId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelDemands_MainDemands_MainDemandId",
                table: "HotelDemands");

            migrationBuilder.DropForeignKey(
                name: "FK_TourDemands_MainDemands_MainDemandId",
                table: "TourDemands");

            migrationBuilder.DropTable(
                name: "RequestChannels");

            migrationBuilder.DropIndex(
                name: "IX_TourDemands_MainDemandId",
                table: "TourDemands");

            migrationBuilder.DropIndex(
                name: "IX_HotelDemands_MainDemandId",
                table: "HotelDemands");
        }
    }
}
