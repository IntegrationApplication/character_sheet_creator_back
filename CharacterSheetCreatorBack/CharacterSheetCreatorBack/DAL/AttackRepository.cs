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
            try
            {
                return _rpgContext.Attacks.First<Attack>(x => x.Id == idAttack);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new InvalidOperationException("The attack doesn't exist.");
            }
        }

        /**********************************************************************/
        /* update                                                             */
        /**********************************************************************/

        public int UpdateAttack(Attack attack)
        {
            try
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
                    dbAttack.LinkedAbility = attack.LinkedAbility;
                    dbAttack.DamageType = attack.DamageType;
                    dbAttack.NbDices = attack.NbDices;
                    dbAttack.DicesFaces = attack.DicesFaces;
                    dbAttack.DamageBonus = attack.DamageBonus;

                    // update the db and save changes
                    _rpgContext.Attacks.Update(dbAttack);
                    _rpgContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new InvalidOperationException("The player doesn't exist.");
            }
            return attack.Id;
        }

        /**********************************************************************/
        /* create                                                             */
        /**********************************************************************/

        public int CreateAttack()
        {
            try
            {
                Attack newAttack = new Attack();
                _rpgContext.Attacks.Add(newAttack);
                _rpgContext.SaveChanges();
                return newAttack.Id;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int CreateAttack(Attack attack)
        {
            try
            {
                // TODO: maybe not like this
                _rpgContext.Attacks.Add(attack);
                _rpgContext.SaveChanges();
                return attack.Id;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeleteAttack(int idAttack)
        {
            try
            {
                Attack toRemove = GetAttack(idAttack);
                // suppression
                _rpgContext.Attacks.Remove(toRemove);
                _rpgContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Impossible to delete this character: " + e.Message);
            }
        }
    }
}

