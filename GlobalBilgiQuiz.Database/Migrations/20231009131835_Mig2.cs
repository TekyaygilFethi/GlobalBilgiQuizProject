using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalBilgiQuiz.Database.Migrations
{
    /// <inheritdoc />
    public partial class Mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence(
                name: "OrderSequence",
                startValue: 4L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence(
                name: "OrderSequence",
                startValue: 6L);
        }
    }
}
