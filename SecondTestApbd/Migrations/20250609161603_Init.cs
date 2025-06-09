using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondTestApbd.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Racers",
                columns: table => new
                {
                    RacerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racers", x => x.RacerId);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.RaceId);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LengthInKm = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackId);
                });

            migrationBuilder.CreateTable(
                name: "TrackRaces",
                columns: table => new
                {
                    TrackRaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    BestTimeInSeconds = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackRaces", x => x.TrackRaceId);
                    table.ForeignKey(
                        name: "FK_TrackRaces_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackRaces_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceParticipations",
                columns: table => new
                {
                    TrackRaceId = table.Column<int>(type: "int", nullable: false),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    FinishTimeInSeconds = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceParticipations", x => new { x.TrackRaceId, x.RacerId });
                    table.ForeignKey(
                        name: "FK_RaceParticipations_Racers_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racers",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceParticipations_TrackRaces_TrackRaceId",
                        column: x => x.TrackRaceId,
                        principalTable: "TrackRaces",
                        principalColumn: "TrackRaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaceParticipations_RacerId",
                table: "RaceParticipations",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackRaces_RaceId",
                table: "TrackRaces",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackRaces_TrackId",
                table: "TrackRaces",
                column: "TrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceParticipations");

            migrationBuilder.DropTable(
                name: "Racers");

            migrationBuilder.DropTable(
                name: "TrackRaces");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}
