using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterSheetCreatorBack.Migrations
{
    public partial class billy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Classes_ClasseName",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "CharacterID",
                table: "Spells",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CharacterID",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClasseName",
                table: "Characters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "CharacterID",
                table: "Attacks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CharacterID",
                table: "Abilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spells_CharacterID",
                table: "Spells",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CharacterID",
                table: "Skills",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_CharacterID",
                table: "Attacks",
                column: "CharacterID");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_CharacterID",
                table: "Abilities",
                column: "CharacterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_Characters_CharacterID",
                table: "Abilities",
                column: "CharacterID",
                principalTable: "Characters",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_Characters_CharacterID",
                table: "Attacks",
                column: "CharacterID",
                principalTable: "Characters",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Classes_ClasseName",
                table: "Characters",
                column: "ClasseName",
                principalTable: "Classes",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Characters_CharacterID",
                table: "Skills",
                column: "CharacterID",
                principalTable: "Characters",
                principalColumn: "ID");

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
                name: "FK_Abilities_Characters_CharacterID",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_Characters_CharacterID",
                table: "Attacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Classes_ClasseName",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Characters_CharacterID",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Characters_CharacterID",
                table: "Spells");

            migrationBuilder.DropIndex(
                name: "IX_Spells_CharacterID",
                table: "Spells");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CharacterID",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Attacks_CharacterID",
                table: "Attacks");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_CharacterID",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "CharacterID",
                table: "Spells");

            migrationBuilder.DropColumn(
                name: "CharacterID",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CharacterID",
                table: "Attacks");

            migrationBuilder.DropColumn(
                name: "CharacterID",
                table: "Abilities");

            migrationBuilder.AlterColumn<string>(
                name: "ClasseName",
                table: "Characters",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Classes_ClasseName",
                table: "Characters",
                column: "ClasseName",
                principalTable: "Classes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
