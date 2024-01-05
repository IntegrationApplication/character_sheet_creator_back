using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetCreatorBack.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGame = table.Column<int>(type: "int", nullable: false),
                    IdPlayer = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Ac = table.Column<int>(type: "int", nullable: false),
                    SpellSaveDC = table.Column<int>(type: "int", nullable: false),
                    SpellCastAbility = table.Column<int>(type: "int", nullable: false),
                    Initiative = table.Column<int>(type: "int", nullable: false),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    HpMax = table.Column<int>(type: "int", nullable: false),
                    HitDiceNumber = table.Column<int>(type: "int", nullable: false),
                    HitDiceValue = table.Column<int>(type: "int", nullable: false),
                    Stats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proefficiencies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProefficiencyBonus = table.Column<int>(type: "int", nullable: false),
                    PassivePerception = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedAbility = table.Column<int>(type: "int", nullable: false),
                    DamageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NbDices = table.Column<int>(type: "int", nullable: false),
                    DicesFaces = table.Column<int>(type: "int", nullable: false),
                    DamageBonus = table.Column<int>(type: "int", nullable: false),
                    CharacterModelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_Characters_CharacterModelID",
                        column: x => x.CharacterModelID,
                        principalTable: "Characters",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_CharacterModelID",
                table: "Attacks",
                column: "CharacterModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
