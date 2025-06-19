using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SpilAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brugere",
                columns: table => new
                {
                    BrugerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brugernavn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brugere", x => x.BrugerId);
                });

            migrationBuilder.CreateTable(
                name: "Spil",
                columns: table => new
                {
                    SpilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Navn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spil", x => x.SpilId);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrugerId = table.Column<int>(type: "int", nullable: false),
                    SpilId = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Dato = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreId);
                    table.ForeignKey(
                        name: "FK_Scores_Brugere_BrugerId",
                        column: x => x.BrugerId,
                        principalTable: "Brugere",
                        principalColumn: "BrugerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Spil_SpilId",
                        column: x => x.SpilId,
                        principalTable: "Spil",
                        principalColumn: "SpilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Spil",
                columns: new[] { "SpilId", "Navn" },
                values: new object[,]
                {
                    { 1, "Boldspil" },
                    { 2, "Puslespil" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brugere_Brugernavn",
                table: "Brugere",
                column: "Brugernavn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_BrugerId",
                table: "Scores",
                column: "BrugerId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SpilId",
                table: "Scores",
                column: "SpilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Brugere");

            migrationBuilder.DropTable(
                name: "Spil");
        }
    }
}
