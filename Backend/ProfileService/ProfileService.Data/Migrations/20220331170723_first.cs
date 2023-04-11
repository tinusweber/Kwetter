using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    ProfilePictureBase64 = table.Column<string>(type: "text", nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: false),
                    FollowingUsers = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    BlockedUsers = table.Column<List<Guid>>(type: "uuid[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.OwnerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
