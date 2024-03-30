using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DAL;
using CharacterSheetCreatorBack.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Xml.Linq;

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
        public IActionResult GetCharacter(ulong idPlayer, ulong idGame)
        {
            try
            {
                Character? character = _characterRepo.GetCharacter(idPlayer, idGame);
                if (character is null)
                {
                    return StatusCode(500, "Error: character not found in the database.");
                }
                return Ok(character);
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to fetch the character from the database.");
            }
        }

        /**********************************************************************/
        /* roll                                                               */
        /**********************************************************************/


        [HttpGet("RollInitiative")]
        public IActionResult RollInitiative(ulong idPlayer, ulong idGame)
        {
            try
            {
                Console.WriteLine("roll initiative");
                Character? character = _characterRepo.GetCharacter(idPlayer, idGame);
                if (character is null)
                {
                    return StatusCode(500, "Error: character not found in the database.");
                }
                return Ok(character.RollInitiative());
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to roll initiative.");
            }
        }

        [HttpGet("RollAny")]
        public IActionResult RollAny(ulong idPlayer, ulong idGame, string name)
        {
            try
            {
                Console.WriteLine("roll any");
                Character? character = _characterRepo.GetCharacter(idPlayer, idGame);
                if (character is null)
                {
                    return StatusCode(500, "Error: character not found in the database.");
                }
                return Ok(character.RollAny(name));
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to roll " + name + ".");
            }
        }

        [HttpGet("RollAttack")]
        public IActionResult RollAttack(ulong idPlayer, ulong idGame, int index)
        {
            try
            {
                Console.WriteLine("roll attack");
                Character? character = _characterRepo.GetCharacter(idPlayer, idGame);
                if (character is null)
                {
                    return StatusCode(500, "Error: character not found in the database.");
                }
                return Ok(character.RollAttack(index));
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to roll the attack " + index + ".");
            }
        }

        [HttpGet("RollDamage")]
        public IActionResult RollDamage(ulong idPlayer, ulong idGame, int index)
        {
            try
            {
                Console.WriteLine("roll damage");
                Character? character = _characterRepo.GetCharacter(idPlayer, idGame);
                if (character is null)
                {
                    return StatusCode(500, "Error: character not found in the database.");
                }
                return Ok(character.RollDamage(index));
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to roll the damages for the attack " + index + ".");
            }
        }

        /**********************************************************************/
        /* post                                                               */
        /**********************************************************************/

        [HttpPost("CreateCharacter")]
        public IActionResult CreateCharacter(ulong idPlayer, ulong idGame) {
            try
            {
                try
                {
                    // it should return null if the character isn't in the db but apparently it throws an error.
                    Character? character = _characterRepo.GetCharacter(idPlayer, idGame);
                    return StatusCode(500, "Error: this character is already created.");
                }
                catch
                {
                    Console.WriteLine("create character");
                    int id = _characterRepo.CreateCharacter(idPlayer, idGame);
                    return Ok(id);
                }
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to create a new character.");
            }
        }

        /**********************************************************************/
        /* put                                                                */
        /**********************************************************************/

        [HttpPut("UpdateCharacter")]
        public IActionResult UpdateCharacter(Character character) {
            try
            {
                Console.WriteLine("update character");
                character.Print();
                // update
                int id = _characterRepo.UpdateCharacter(character);
                return Ok(id);
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to update the character.");
            }
        }

        [HttpPut("TakeDamage")]
        public IActionResult TakeDamage(ulong idPlayer, ulong idGame, int amount) {
            try
            {
                Console.WriteLine("take damages");
                Character? character = _characterRepo.GetCharacter(idPlayer, idGame);
                if (character is null)
                {
                    return StatusCode(500, "Error: character not found in the database.");
                }
                character.Hp -= amount;
                if (character.Hp < 0)
                {
                    character.Hp = 0;
                }
                _characterRepo.UpdateCharacter(character);
                return Ok(character.Hp);
            }
            catch
            {
                return StatusCode(500, "Error: something went wrong when trying to take damages.");
            }
        }

        /**********************************************************************/
        /* delete                                                             */
        /**********************************************************************/

        [HttpDelete("DeleteCharacter")]
        public IActionResult DeleteCharacter(ulong idPlayer, ulong idGame) {
            _characterRepo.DeleteCharacter(idPlayer, idGame);
            Console.WriteLine("delete character");
            return StatusCode(200);
        }
    }
}
