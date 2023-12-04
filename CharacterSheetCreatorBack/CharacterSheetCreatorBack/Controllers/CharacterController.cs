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
        public Task<Character> GetCharacter(int idPlayer, int idCharacter)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character);
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/

        [HttpGet("RollAbility")]
        public Task<Character> RollAbility(int idPlayer, int idCharacter, string abilityName)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollAbility());
        }

        [HttpGet("RollSkill")]
        public Task<Character> RollAbility(int idPlayer, int idCharacter, string abilityName)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollAbility());
        }

        [HttpGet("RollInitiative")]
        public Task<Character> RollInitiative(int idPlayer, int idCharacter)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollInitiative());
        }

        [HttpGet("RollAttack")]
        public Task<Character> RollAttack(int idPlayer, int idCharacter, int index)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollAttack(index));
        }

        /**********************************************************************/
        /* post                                                               */
        /**********************************************************************/

        [HttpPost("CreateCharacter")]
        public Task<int> CreateCharacter(Character character) {
            int id = _characterRepo.CreateCharacter(character);
            return Task.FromResult(id);
        }

        /**********************************************************************/
        /* put                                                                */
        /**********************************************************************/

        [HttpPost("UpdateCharacter")]
        public Task<int> UpdateCharacter(Character character) {
            int id = _characterRepo.UpdateCharacter(character);
            return Task.FromResult(id);
        }

        [HttpPost("TakeDamage")]
        public Task<int> TakeDamage(int idPlayer, int idCharacter, int amount) {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            character.HP -= amount;
            if (character.HP < 0)
            {
                character.HP = 0;
            }
            _characterRepo.UpdateCharacter(character);
            return Task.FromResult(character.HP);
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        [HttpPost("DeleteCharacter")]
        public IActionResult DeleteCharacter(int idPlayer, int idCharacter) {
            _characterRepo.DeleteCharacter(idPlayer, idCharacter);
            return StatusCode(200);
        }
    }
}
