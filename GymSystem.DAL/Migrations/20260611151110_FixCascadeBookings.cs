using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_gymUsers_MemberId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_sessions_SessionId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_gymUsers_TrainerId",
                table: "sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_gymUsers_MemberId",
                table: "bookings",
                column: "MemberId",
                principalTable: "gymUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_sessions_SessionId",
                table: "bookings",
                column: "SessionId",
                principalTable: "sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_gymUsers_TrainerId",
                table: "sessions",
                column: "TrainerId",
                principalTable: "gymUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_gymUsers_MemberId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_sessions_SessionId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_gymUsers_TrainerId",
                table: "sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_gymUsers_MemberId",
                table: "bookings",
                column: "MemberId",
                principalTable: "gymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_sessions_SessionId",
                table: "bookings",
                column: "SessionId",
                principalTable: "sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_gymUsers_TrainerId",
                table: "sessions",
                column: "TrainerId",
                principalTable: "gymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
