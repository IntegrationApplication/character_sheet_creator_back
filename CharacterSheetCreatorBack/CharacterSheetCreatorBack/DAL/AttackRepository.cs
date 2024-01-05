using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DbContexts;

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
            return _rpgContext.Attacks.First<Attack>(x => x.Id == idAttack);
        }

        /**********************************************************************/
        /* update                                                             */
        /**********************************************************************/

        public int UpdateAttack(Attack attack)
        {
            // on ne peut pas appeler Update sur le character pris en paramètre
            // (la méthode Update regarde la référence de l'objet).
            Attack? dbAttack = GetAttack(attack.Id);

            if (dbAttack is null)
            {
                // ajout si existe pas (plus simple pour update le character)
                CreateAttack(attack);
            }
            else
            {
                // update the db object
                dbAttack.Update(attack);

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
            Attack newAttack = new Attack();
            _rpgContext.Attacks.Add(newAttack);
            _rpgContext.SaveChanges();
            return newAttack.Id;
        }

        public int CreateAttack(Attack attack)
        {
            Console.WriteLine("create attack");
            Attack newAttack = new Attack();
            newAttack.Update(attack);
            _rpgContext.Attacks.Add(newAttack);
            _rpgContext.SaveChanges();
            return attack.Id;
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeleteAttack(int idAttack)
        {
            Attack? toRemove = GetAttack(idAttack);
            // suppression
            _rpgContext.Attacks.Remove(toRemove);
            _rpgContext.SaveChanges();
        }
    }
}

