using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations.Pg
{
    public partial class AddedRequestChannelDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "RequestChannels",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "RequestChannels");
        }
    }
}
