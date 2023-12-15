using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DAL;
using CharacterSheetCreatorBack.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace CharacterSheetCreatorBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly RpgContext _rpgContext;
        private readonly CharacterRepository _characterRepo;
        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ILogger<CharacterController> logger, RpgContext rpgContext)
        {
            _logger = logger;
            _rpgContext = rpgContext;
            _characterRepo = new CharacterRepository(_rpgContext);
        }

        /**********************************************************************/
        /* get                                                                */
        /**********************************************************************/

        [HttpGet("GetCharacter")]
        public Task<Character> GetCharacter(int idPlayer, int idGame)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idGame);
            return Task.FromResult(character);
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/


        [HttpGet("RollInitiative")]
        public Task<int> RollInitiative(int idPlayer, int idGame)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idGame);
            return Task.FromResult(character.RollInitiative());
        }

        [HttpGet("RollAny")]
        public Task<int> RollAny(int idPlayer, int idGame, string name)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idGame);
            return Task.FromResult(character.RollAny(name));
        }

        [HttpGet("RollAttack")]
        public Task<int> RollAttack(int idPlayer, int idGame, int index)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idGame);
            return Task.FromResult(character.RollAttack(index));
        }

        [HttpGet("RollDamage")]
        public Task<int> RollDamage(int idPlayer, int idGame, int index)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idGame);
            return Task.FromResult(character.RollDamage(index));
        }

        /**********************************************************************/
        /* post                                                               */
        /**********************************************************************/

        [HttpPost("CreateCharacter")]
        public Task<int> CreateCharacter(int idPlayer, int idGame) {
            int id = _characterRepo.CreateCharacter(idPlayer, idGame) ;
            return Task.FromResult(id);
        }

        /**********************************************************************/
        /* put                                                                */
        /**********************************************************************/

        [HttpPut("UpdateCharacter")]
        public Task<int> UpdateCharacter(Character character) {
            int id = _characterRepo.UpdateCharacter(character);
            return Task.FromResult(id);
        }

        [HttpPut("TakeDamage")]
        public Task<int> TakeDamage(int idPlayer, int idGame, int amount) {
            Character character = _characterRepo.GetCharacter(idPlayer, idGame);
            character.Hp -= amount;
            if (character.Hp < 0)
            {
                character.Hp = 0;
            }
            _characterRepo.UpdateCharacter(character);
            return Task.FromResult(character.Hp);
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        [HttpDelete("DeleteCharacter")]
        public IActionResult DeleteCharacter(int idPlayer, int idGame) {
            _characterRepo.DeleteCharacter(idPlayer, idGame);
            return StatusCode(200);
        }
    }
}
