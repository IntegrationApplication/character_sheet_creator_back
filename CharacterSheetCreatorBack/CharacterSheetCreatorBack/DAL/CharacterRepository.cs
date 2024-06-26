using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DAL.Models;
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

        public Character? GetCharacter(ulong idPlayer, ulong idGame)
        {
            CharacterModel? model =  _rpgContext.Characters.First<CharacterModel>(x =>
                    x.IdGame == idGame && x.IdPlayer == idPlayer);

            if (model is null)
            {
                return null;
            }

            Character character = model.ToCharacter();
            List<AttackModel> attacks = _rpgContext.Attacks.Where(a => a.CharacterModelId == model.ID).ToList();

            attacks.ForEach(attack => character.Attacks.Add(new Attack(attack)));
            return character;
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
            List<AttackModel> attacks = _rpgContext.Attacks.Where(a => a.CharacterModelId == dbCharacter.ID).ToList();

            if (dbCharacter is null)
            {
                throw new InvalidOperationException("Error: the character doesn't exist.");
            }

            Console.WriteLine("delete attacks");

            // suppression de toutes les attacks existantes
            attacks.ForEach(attack => _attackRepository.DeleteAttack(attack.Id));
            dbCharacter.FromCharacter(character);

            Console.WriteLine("update attacks");

            // on recrée les attacks
            character.Attacks.ForEach(attack =>
            {
                attack.Print();
                _attackRepository.CreateAttack(attack, ref dbCharacter);
            });

            Console.WriteLine("update id");

            // make sure that we have the good id for the output
            character.ID = dbCharacter.ID;

            Console.WriteLine("context update");

            // update the db and save changes
            _rpgContext.Characters.Update(dbCharacter);
            _rpgContext.SaveChanges();
            return character.ID;
        }

        /**********************************************************************/
        /* create                                                             */
        /**********************************************************************/

        public int CreateCharacter(ulong IdPlayer,  ulong IdGame)
        {
            Character newCharacter = new Character(IdPlayer, IdGame);
            CharacterModel model = new CharacterModel(newCharacter);

            _rpgContext.Characters.Add(new CharacterModel(newCharacter));
            _rpgContext.SaveChanges();

            return newCharacter.ID;
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeleteCharacter(ulong idPlayer, ulong idGame)
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

