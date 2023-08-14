using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebDB.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "steamGenerators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SuperheatedSteam_Expenditure = table.Column<int>(type: "integer", nullable: false),
                    SuperheatedSteam_Pressure1 = table.Column<int>(type: "integer", nullable: false),
                    SuperheatedSteam_Pressure2 = table.Column<int>(type: "integer", nullable: false),
                    SuperheatedSteam_Temperature = table.Column<int>(type: "integer", nullable: false),
                    FeedWater_Expenditure = table.Column<int>(type: "integer", nullable: false),
                    FeedWater_Pressure = table.Column<int>(type: "integer", nullable: false),
                    FeedWater_Temperature = table.Column<int>(type: "integer", nullable: false),
                    Gas_Expenditure = table.Column<int>(type: "integer", nullable: false),
                    Gas_Pressure1 = table.Column<int>(type: "integer", nullable: false),
                    Gas_Pressure2 = table.Column<int>(type: "integer", nullable: false),
                    Gas_Temperature = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_steamGenerators", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "steamGenerators");
        }
    }
}
