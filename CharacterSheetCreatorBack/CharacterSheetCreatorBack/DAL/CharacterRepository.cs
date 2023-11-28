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
                return _rpgContext.Characters.First<Character>(x => x.ID == idCharacter && x.IdPlayer == idPlayer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new InvalidOperationException("The player doesn't exist.");
            }
        }

        /**********************************************************************/
        /* update                                                             */
        /**********************************************************************/

        public int CreateOrUpdateCharacter(Character character)
        {
            try
            {
                using var transaction = _rpgContext.Database.BeginTransaction();

                // on ne peut pas appeler Update sur le player pris en paramètre
                // (la méthode Update regarde la référence de l'objet).
                Character dbCharacter = GetCharacter(character.IdPlayer, character.ID);
                dbCharacter.ID = character.ID;
                // TODO: ...
                _rpgContext.Characters.Update(dbCharacter);

                // on valide les changements dans la db
                _rpgContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new InvalidOperationException("The player doesn't exist.");
            }
            return character.ID;
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeleteCharacter(Character character)
        {
            try
            {
                using var transaction = _rpgContext.Database.BeginTransaction();

                Character toRemove = GetCharacter(character.IdPlayer, character.ID);
                // TODO: supprimer les champs compexes

                // suppression
                _rpgContext.Characters.Remove(toRemove);
                _rpgContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Impossible to delete this character: " + e.Message);
            }
        }
    }
}

