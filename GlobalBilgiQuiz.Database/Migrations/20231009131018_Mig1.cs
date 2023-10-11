using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlobalBilgiQuiz.Database.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "OrderSequence",
                startValue: 6L);

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR OrderSequence"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    IsTrue = table.Column<bool>(type: "bit", nullable: false),
                    ChosenCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrueCount = table.Column<int>(type: "int", nullable: false),
                    FalseCount = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metrics_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Order", "Text" },
                values: new object[,]
                {
                    { 1, 1, "Mustafa Kemal Atatürk kaç yılınca doğmuştur?" },
                    { 2, 2, "Hangisi 2. Dünya Savaşı döneminde yaşamıştır?" },
                    { 3, 3, "Futbolda geçen senenin şampiyonu kim oldu?" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "ChosenCount", "IsTrue", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, 0, true, 1, "1881" },
                    { 2, 0, false, 1, "1991" },
                    { 3, 0, false, 1, "1923" },
                    { 4, 0, false, 1, "1900" },
                    { 5, 0, false, 2, "Seda Sayan" },
                    { 6, 0, false, 2, "İbrahim Tatlıses" },
                    { 7, 0, true, 2, "Winston Churcill" },
                    { 8, 0, false, 2, "Türkcan Keskin" },
                    { 9, 0, false, 3, "Beşiktaş" },
                    { 10, 0, false, 3, "Fenerbahçe" },
                    { 11, 0, false, 3, "Trabzonspor" },
                    { 12, 0, true, 3, "Galatasaray" }
                });

            migrationBuilder.InsertData(
                table: "Metrics",
                columns: new[] { "Id", "FalseCount", "QuestionId", "TrueCount" },
                values: new object[,]
                {
                    { 1, 0, 1, 0 },
                    { 2, 0, 2, 0 },
                    { 3, 0, 3, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_QuestionId",
                table: "Metrics",
                column: "QuestionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropSequence(
                name: "OrderSequence");
        }
    }
}
