using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBased_IOT_Platform.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calibrations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    RestingHeartRate = table.Column<double>(type: "REAL", nullable: false),
                    HRV = table.Column<double>(type: "REAL", nullable: false),
                    BreathingRate = table.Column<double>(type: "REAL", nullable: false),
                    Mood = table.Column<string>(type: "TEXT", nullable: false),
                    TracksJson = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibrations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    FitbitAccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    FitbitRefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    FitbitTokenExpiry = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SpotifyAccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    SpotifyRefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    SpotifyTokenExpiry = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calibrations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
