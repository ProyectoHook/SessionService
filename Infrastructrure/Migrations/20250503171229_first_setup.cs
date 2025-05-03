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
                name: "Session",
                columns: table => new
                {
                    idSession = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    acces_code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idParticipant = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    interation_count = table.Column<int>(type: "int", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    max_participants = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    presentation_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.idSession);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    idParticipant = table.Column<int>(type: "int", nullable: false),
                    idUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    connectionStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activityStatus = table.Column<bool>(type: "bit", nullable: false),
                    connectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.idParticipant);
                    table.ForeignKey(
                        name: "FK_Participant_Session_idParticipant",
                        column: x => x.idParticipant,
                        principalTable: "Session",
                        principalColumn: "idSession",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Session");
        }
    }
}
