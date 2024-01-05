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
            CharacterModel? model =  _rpgContext.Characters.First<CharacterModel>(x =>
                    x.IdGame == idGame && x.IdPlayer == idPlayer);
            return model.ToCharacter();
        }

        /**********************************************************************/
        /* update                                                             */
        /**********************************************************************/

        public int UpdateCharacter(Character character)
        {
            // on ne peut pas appeler Update sur le character pris en paramètre
            // (la méthode Update regarde la référence de l'objet).
            CharacterModel? dbCharacter = _rpgContext.Characters.First<CharacterModel>(x =>
                    x.IdGame == character.IdGame && x.IdPlayer == character.IdPlayer);

            if (dbCharacter is null)
            {
                throw new InvalidOperationException("Error: the character doesn't exist.");
            }

            // suppression de toutes les attacks existantes
            dbCharacter.Attacks.ForEach(attack => _attackRepository.DeleteAttack(attack.Id));
            dbCharacter.Attacks.Clear();

            dbCharacter.FromCharacter(character);

            // on recrée les attacks
            dbCharacter.Attacks.ForEach(attack =>
            {
                attack.Print();
                _attackRepository.CreateAttack(attack);
            });

            // make sure that we have the good id for the output
            character.ID = dbCharacter.ID;

            // update the db and save changes
            _rpgContext.Characters.Update(dbCharacter);
            _rpgContext.SaveChanges();
            return character.ID;
        }

        /**********************************************************************/
        /* create                                                             */
        /**********************************************************************/

        public int CreateCharacter(int IdPlayer,  int IdGame)
        {
            Character newCharacter = new Character
            {
                IdPlayer = IdPlayer,
                IdGame = IdGame
            };
            CharacterModel model = new CharacterModel(newCharacter);

            _rpgContext.Characters.Add(new CharacterModel(newCharacter));
            _rpgContext.SaveChanges();

            return newCharacter.ID;
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeleteCharacter(int idPlayer, int idGame)
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
    }
}

