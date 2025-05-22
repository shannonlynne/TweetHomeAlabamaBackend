using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TweetHomeAlabama.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Bird");

            migrationBuilder.CreateTable(
                name: "BirdShape",
                schema: "Bird",
                columns: table => new
                {
                    BirdShapeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdShape", x => x.BirdShapeId);
                });

            migrationBuilder.CreateTable(
                name: "BirdSize",
                schema: "Bird",
                columns: table => new
                {
                    BirdSizeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdSize", x => x.BirdSizeId);
                });

            migrationBuilder.CreateTable(
                name: "Bird",
                schema: "Bird",
                columns: table => new
                {
                    BirdId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ShortDescription = table.Column<string>(type: "TEXT", nullable: false),
                    ShapeId = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bird", x => x.BirdId);
                    table.ForeignKey(
                        name: "FK_Bird_BirdShape_ShapeId",
                        column: x => x.ShapeId,
                        principalSchema: "Bird",
                        principalTable: "BirdShape",
                        principalColumn: "BirdShapeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bird_BirdSize_SizeId",
                        column: x => x.SizeId,
                        principalSchema: "Bird",
                        principalTable: "BirdSize",
                        principalColumn: "BirdSizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BirdColor",
                schema: "Bird",
                columns: table => new
                {
                    BirdColorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BirdEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdColor", x => x.BirdColorId);
                    table.ForeignKey(
                        name: "FK_BirdColor_Bird_BirdEntityId",
                        column: x => x.BirdEntityId,
                        principalSchema: "Bird",
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                });

            migrationBuilder.CreateTable(
                name: "BirdHabitat",
                schema: "Bird",
                columns: table => new
                {
                    BirdHabitatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BirdEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdHabitat", x => x.BirdHabitatId);
                    table.ForeignKey(
                        name: "FK_BirdHabitat_Bird_BirdEntityId",
                        column: x => x.BirdEntityId,
                        principalSchema: "Bird",
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bird_ShapeId",
                schema: "Bird",
                table: "Bird",
                column: "ShapeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_SizeId",
                schema: "Bird",
                table: "Bird",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_BirdColor_BirdEntityId",
                schema: "Bird",
                table: "BirdColor",
                column: "BirdEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BirdHabitat_BirdEntityId",
                schema: "Bird",
                table: "BirdHabitat",
                column: "BirdEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirdColor",
                schema: "Bird");

            migrationBuilder.DropTable(
                name: "BirdHabitat",
                schema: "Bird");

            migrationBuilder.DropTable(
                name: "Bird",
                schema: "Bird");

            migrationBuilder.DropTable(
                name: "BirdShape",
                schema: "Bird");

            migrationBuilder.DropTable(
                name: "BirdSize",
                schema: "Bird");
        }
    }
}
