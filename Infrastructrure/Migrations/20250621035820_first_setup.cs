using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructrure.Migrations
{
    /// <inheritdoc />
    public partial class first_setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccesCode",
                columns: table => new
                {
                    idCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesCode", x => x.idCode);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    idSession = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    access_code = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    interation_count = table.Column<int>(type: "int", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    max_participants = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    presentation_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    currentSlide = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.idSession);
                    table.ForeignKey(
                        name: "FK_Session_AccesCode_access_code",
                        column: x => x.access_code,
                        principalTable: "AccesCode",
                        principalColumn: "idCode");
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    idParticipant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    connectionStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activityStatus = table.Column<bool>(type: "bit", nullable: false),
                    connectionId = table.Column<int>(type: "int", nullable: true),
                    idSession = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.idParticipant);
                    table.ForeignKey(
                        name: "FK_Participant_Session_idSession",
                        column: x => x.idSession,
                        principalTable: "Session",
                        principalColumn: "idSession");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_idSession",
                table: "Participant",
                column: "idSession");

            migrationBuilder.CreateIndex(
                name: "IX_Session_access_code",
                table: "Session",
                column: "access_code",
                unique: true,
                filter: "[access_code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "AccesCode");
        }
    }
}
