using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DAL.Models;
using CharacterSheetCreatorBack.DbContexts;
using CharacterSheetCreatorBack.Models;
using System.Linq;

namespace CharacterSheetCreatorBack.DAL
{

    public class AttackRepository
    {
        private readonly RpgContext _rpgContext;

        public AttackRepository(RpgContext rpgContext) {
            this._rpgContext = rpgContext;
        }

        /**********************************************************************/
        /* get                                                                */
        /**********************************************************************/

        public Attack? GetAttack(int idAttack)
        {
            Attack attack = new Attack();
            AttackModel? model = _rpgContext.Attacks.First<AttackModel>(x => x.Id == idAttack);
            attack.FromModel(model);
            return attack;
        }

        /**********************************************************************/
        /* update                                                             */
        /**********************************************************************/

        public int UpdateAttack(Attack attack)
        {
            // on ne peut pas appeler Update sur le character pris en paramètre
            // (la méthode Update regarde la référence de l'objet).
            AttackModel? dbAttack = _rpgContext.Attacks.First<AttackModel>(x => x.Id == attack.Id);

            if (dbAttack is null)
            {
                // ajout si existe pas (plus simple pour update le character)
                throw new InvalidOperationException("Error: the attack doesn't exist.");
            }
            else
            {
                // update the db object
                dbAttack.FromAttack(attack);

                // update the db and save changes
                _rpgContext.Attacks.Update(dbAttack);
                _rpgContext.SaveChanges();
            }
            return attack.Id;
        }

        /**********************************************************************/
        /* create                                                             */
        /**********************************************************************/

        public int CreateAttack()
        {
            AttackModel model = new AttackModel();
            _rpgContext.Attacks.Add(model);
            _rpgContext.SaveChanges();
            return model.Id;
        }

        public int CreateAttack(Attack attack, ref CharacterModel parent)
        {
            Console.WriteLine("create attack");
            AttackModel model = new AttackModel();
            model.FromAttack(attack);
            model.CharacterModelId = parent.ID;
            model.CharacterModel = parent;
            _rpgContext.Attacks.Add(model);
            _rpgContext.SaveChanges();
            return attack.Id;
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeleteAttack(int idAttack)
        {
            AttackModel? toRemove = _rpgContext.Attacks.First<AttackModel>(x => x.Id == idAttack);
            // suppression
            _rpgContext.Attacks.Remove(toRemove);
            _rpgContext.SaveChanges();
        }
    }
}

