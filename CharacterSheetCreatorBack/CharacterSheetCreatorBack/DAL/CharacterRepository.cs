using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DbContexts;
using CharacterSheetCreatorBack.Models;

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
                CharacterModel? model =  _rpgContext.Characters.First<CharacterModel>(x =>
                        x.IdGame == idGame && x.IdPlayer == idPlayer);
                return model.ToCharacter();
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
                CharacterModel? dbCharacter = _rpgContext.Characters.First<CharacterModel>(x =>
                        x.IdGame == character.IdGame && x.IdPlayer == character.IdPlayer);

                // la façon la plus simple de mettre à jour les attacks
                dbCharacter.Attacks.ForEach(x => _attackRepository.DeleteAttack(x.Id));
                dbCharacter.Attacks.Clear();

                dbCharacter.FromCharacter(character);

                // TODO: faire une vraie update des attacks
                dbCharacter.Attacks.ForEach(x => _attackRepository.CreateAttack(x));

                // NOTE: pas sûr pour ça
                /* dbCharacter.Attacks.Clear(); */
                /* dbCharacter.Attacks = character.Attacks; */
                /* dbCharacter.Attacks.ForEach(attack => */
                /*         _attackRepository.UpdateAttack(attack)); */

                // make sure that we have the good id for the output
                character.ID = dbCharacter.ID;

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

                _rpgContext.Characters.Add(new CharacterModel(newCharacter));
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

        public void DeleteCharacter(int idPlayer, int idGame)
        {
            try
            {
                CharacterModel? toRemove = _rpgContext.Characters.First<CharacterModel>(x =>
                        x.IdGame == idPlayer && x.IdPlayer == idGame);

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

