using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonDexSharp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreatePokemonSpeciesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseStats_Attack = table.Column<int>(type: "int", nullable: false),
                    BaseStats_Defense = table.Column<int>(type: "int", nullable: false),
                    BaseStats_HP = table.Column<int>(type: "int", nullable: false),
                    BaseStats_SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    BaseStats_SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    BaseStats_Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Species");
        }
    }
}
