using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecondTestApbd.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Racers",
                columns: new[] { "RacerId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Lewis", "Hamilton" },
                    { 2, "Charles", "Leclerc" }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "RaceId", "Date", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silverstone, UK", "British Grand Prix" },
                    { 2, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monte Carlo, Monaco", "Monaco Grand Prix" }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "TrackId", "LengthInKm", "Name" },
                values: new object[,]
                {
                    { 1, 5.89m, "Silverstone Circuit" },
                    { 2, 3.34m, "Monaco Circuit" }
                });

            migrationBuilder.InsertData(
                table: "TrackRaces",
                columns: new[] { "TrackRaceId", "BestTimeInSeconds", "Laps", "RaceId", "TrackId" },
                values: new object[,]
                {
                    { 1, 5400, 52, 1, 1 },
                    { 2, 6200, 78, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "RaceParticipations",
                columns: new[] { "RacerId", "TrackRaceId", "FinishTimeInSeconds", "Position" },
                values: new object[,]
                {
                    { 1, 1, 5460, 1 },
                    { 1, 2, 6300, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RaceParticipations",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RaceParticipations",
                keyColumns: new[] { "RacerId", "TrackRaceId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Racers",
                keyColumn: "RacerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Racers",
                keyColumn: "RacerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrackRaces",
                keyColumn: "TrackRaceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrackRaces",
                keyColumn: "TrackRaceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "TrackId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "TrackId",
                keyValue: 2);
        }
    }
}
