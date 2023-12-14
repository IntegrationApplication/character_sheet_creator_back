using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DbContexts;

namespace CharacterSheetCreatorBack.DAL
{

    public class CharacterRepository
    {
        private readonly RpgContext _rpgContext;
        private AttackRepository _attackRepository;

        public CharacterRepository(RpgContext rpgContext) {
            this._rpgContext = rpgContext;
            this._attackRepository = new AttackRepository(rpgContext);
        }

        /**********************************************************************/
        /* get                                                                */
        /**********************************************************************/

        public Character? GetCharacter(int idPlayer, int idGame)
        {
            try
            {
                return _rpgContext.Characters.First<Character>(x =>
                        x.IdGame == idGame && x.IdPlayer == idPlayer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new InvalidOperationException("The character doesn't exist.");
            }
        }

        /**********************************************************************/
        /* update                                                             */
        /**********************************************************************/

        public int UpdateCharacter(Character character)
        {
            try
            {
                // on ne peut pas appeler Update sur le character pris en paramètre
                // (la méthode Update regarde la référence de l'objet).
                Character dbCharacter = GetCharacter(character.IdPlayer, character.IdGame);

                // update the db object
                dbCharacter.Name = character.Name;
                dbCharacter.ClassName = character.ClassName;
                dbCharacter.Level = character.Level;
                dbCharacter.Ac = character.Ac;
                dbCharacter.SpellSaveDC = character.SpellSaveDC;
                dbCharacter.SpeelCastAbility = character.SpeelCastAbility;
                dbCharacter.Initiative = character.Initiative;
                dbCharacter.Hp = character.Hp;
                dbCharacter.HpMax = character.HpMax;
                dbCharacter.HitDiceNumber = character.HitDiceNumber;
                dbCharacter.HitDiceValue = character.HitDiceValue;
                dbCharacter.Stats = character.Stats;
                dbCharacter.Proefficiencies = character.Proefficiencies;
                dbCharacter.ProefficiencyBonus = character.ProefficiencyBonus;
                dbCharacter.PassivePerception  = character.PassivePerception;

                // NOTE: pas sûr pour ça
                dbCharacter.Attacks.Clear();
                dbCharacter.Attacks = character.Attacks;
                dbCharacter.Attacks.ForEach(attack =>
                        _attackRepository.UpdateAttack(attack));

                // update the db and save changes
                _rpgContext.Characters.Update(dbCharacter);
                _rpgContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new InvalidOperationException("The player doesn't exist.");
            }
            return character.ID;
        }

        /**********************************************************************/
        /* create                                                             */
        /**********************************************************************/

        public int CreateCharacter(int IdPlayer,  int IdGame)
        {
            try
            {
                Character newCharacter = new Character
                {
                    IdPlayer = IdPlayer,
                    IdGame = IdGame
                };

                _rpgContext.Characters.Add(newCharacter);
                _rpgContext.SaveChanges();

                return newCharacter.ID;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeleteCharacter(int idPlayer, int idCharacter)
        {
            try
            {
                Character toRemove = GetCharacter(idPlayer, idCharacter);

                // on supprime les attacks
                toRemove.Attacks.ForEach(x => _attackRepository.DeleteAttack(x.Id));
                toRemove.Attacks.Clear();

                // suppression
                _rpgContext.Characters.Remove(toRemove);
                _rpgContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Impossible to delete this character: " + e.Message);
            }
        }
    }
}

