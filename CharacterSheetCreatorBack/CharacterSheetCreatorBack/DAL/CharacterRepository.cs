using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DbContexts;

namespace CharacterSheetCreatorBack.DAL
{

    public class CharacterRepository
    {
        private readonly RpgContext _rpgContext;

        public CharacterRepository(RpgContext rpgContext) {
            this._rpgContext = rpgContext;
        }

        /**********************************************************************/
        /* get                                                                */
        /**********************************************************************/

        public Character GetCharacter(int idPlayer, int idCharacter)
        {
            try
            {
                return _rpgContext.Characters.First<Character>(x =>
                        x.ID == idCharacter && x.IdPlayer == idPlayer);
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
                Character dbCharacter = GetCharacter(character.IdPlayer, character.ID);
                dbCharacter.ID = character.ID;
                // TODO: ...
                _rpgContext.Characters.Update(dbCharacter);

                // on valide les changements dans la db
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
                // TODO: supprimer les champs compexes

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

