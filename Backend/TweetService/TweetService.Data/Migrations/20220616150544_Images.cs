using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TweetService.Data.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Tweets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Tweets");
        }
    }
}
