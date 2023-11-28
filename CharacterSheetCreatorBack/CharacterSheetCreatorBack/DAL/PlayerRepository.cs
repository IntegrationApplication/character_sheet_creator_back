using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DbContexts;

namespace CharacterSheetCreatorBack.DAL
{

    public class PlayerRepository
    {
        private readonly RpgContext _rpgContext;

        public PlayerRepository(RpgContext rpgContext) {
            this._rpgContext = rpgContext;
        }

        /**********************************************************************/
        /* get                                                                */
        /**********************************************************************/

        public Player GetPlayer(int id)
        {
            try
            {
                return _rpgContext.Players.FirstOrDefault<Player>(x => x.ID == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidOperationException("The player doesn't exist.");
            }
        }

        /**********************************************************************/
        /* update                                                             */
        /**********************************************************************/

        public int CreateOrUpdatePlayer(Player player)
        {
            try
            {
                using var transaction = _rpgContext.Database.BeginTransaction();

                // on ne peut pas appeler Update sur le player pris en paramètre
                // (la méthode Update regarde la référence de l'objet).
                Player dbPlayer = GetPlayer(player.ID);
                dbPlayer.ID = player.ID;
                dbPlayer.IDDiscord = player.IDDiscord;
                dbPlayer.Characters = player.Characters;
                _rpgContext.Players.Update(dbPlayer);

                // on valide les changements dans la db
                _rpgContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidOperationException("The player doesn't exist.");
            }
            return player.ID;
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        public void DeletePlayer(Player player)
        {
            try
            {
                using var transaction = _rpgContext.Database.BeginTransaction();

                Player toRemove = GetPlayer(player.ID);
                // TODO: supprimer les champs compexes

                // suppression
                _rpgContext.Players.Remove(toRemove);
                _rpgContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Impossible to delete this player: " + e.Message);
            }
        }
    }
}
