using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetCreatorBack.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterID",
                table: "Spells",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HitDice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGame = table.Column<int>(type: "int", nullable: false),
                    IdPlayer = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClasseName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Ac = table.Column<int>(type: "int", nullable: false),
                    SpellSaveDC = table.Column<int>(type: "int", nullable: false),
                    SpeelCastAbility = table.Column<int>(type: "int", nullable: false),
                    Initiative = table.Column<int>(type: "int", nullable: false),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    HpMax = table.Column<int>(type: "int", nullable: false),
                    HitDice = table.Column<int>(type: "int", nullable: false),
                    ProefficiencyBonus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Characters_Classes_ClasseName",
                        column: x => x.ClasseName,
                        principalTable: "Classes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Modifier = table.Column<int>(type: "int", nullable: false),
                    CharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Abilities_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedAbilityName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DamageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamageDice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_Abilities_LinkedAbilityName",
                        column: x => x.LinkedAbilityName,
                        principalTable: "Abilities",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Attacks_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AbilityName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Modifier = table.Column<int>(type: "int", nullable: false),
                    Proefficiency = table.Column<bool>(type: "bit", nullable: false),
                    CharacterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Skills_Abilities_AbilityName",
                        column: x => x.AbilityName,
                        principalTable: "Abilities",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Skills_Characters_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Characters",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spells_CharacterID",
                table: "Spells",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_CharacterID",
                table: "Abilities",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_CharacterID",
                table: "Attacks",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_LinkedAbilityName",
                table: "Attacks",
                column: "LinkedAbilityName");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ClasseName",
                table: "Characters",
                column: "ClasseName");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_AbilityName",
                table: "Skills",
                column: "AbilityName");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CharacterID",
                table: "Skills",
                column: "CharacterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Characters_CharacterID",
                table: "Spells",
                column: "CharacterID",
                principalTable: "Characters",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Characters_CharacterID",
                table: "Spells");

            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Spells_CharacterID",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "CharacterID",
                table: "Spells");
        }
    }
}
