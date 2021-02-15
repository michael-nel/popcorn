using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(maxLength: 250, nullable: false),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Chairs = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SessionStart = table.Column<DateTime>(nullable: false),
                    SessionEnd = table.Column<DateTime>(nullable: false),
                    TicketValue = table.Column<decimal>(nullable: false),
                    Animation = table.Column<int>(nullable: false),
                    Audio = table.Column<int>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "Chairs", "Name" },
                values: new object[,]
                {
                    { new Guid("a0716b31-2866-492d-9cfa-efb97fdfc053"), 50, "Sala 1" },
                    { new Guid("49c8c7b2-c769-4103-bbab-977f6521887d"), 50, "Sala 2" },
                    { new Guid("f1dea0ef-40ea-422b-9d8b-9c59d3d55ab9"), 50, "Sala 3" },
                    { new Guid("39ced579-5270-4b0d-a5c4-3004adccc95b"), 50, "Sala 4" },
                    { new Guid("1734970d-f700-4087-91dd-3c67d434cbcf"), 50, "Sala 5" },
                    { new Guid("2e0729ab-ff22-45c4-892f-98ee134a337b"), 50, "Sala 6" },
                    { new Guid("da0509ee-be35-4474-bbc5-6e12c3db97a6"), 50, "Sala 7" },
                    { new Guid("eb5ef3bf-f2d6-4e3f-ae0d-70cbc03dc8bb"), 50, "Sala 8" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_MovieId",
                table: "Session",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_RoomId",
                table: "Session",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
