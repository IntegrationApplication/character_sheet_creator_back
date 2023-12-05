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

        [HttpGet(Name = "GetSpell")]
        public ActionResult Get()
        {

            try
            {
                var query = from b in _rpgContext.Spells
                            select b;
                List<Spell> Spells = new List<Spell>();

                foreach (var item in query)
                {
                    Spells.Add(item);
                }
                return Ok(Spells);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public Task<int> RollAbility(int idPlayer, int idCharacter, string abilityName)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollAbility(abilityName));
        }

        [HttpGet("RollSkill")]
        public Task<int> RollSkill(int idPlayer, int idCharacter, string skillName)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollAbility(skillName));
        }

        [HttpGet("RollInitiative")]
        public Task<int> RollInitiative(int idPlayer, int idCharacter)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollInitiative());
        }

        [HttpGet("RollAny")]
        public Task<int> RollAny(int idPlayer, int idCharacter, string name)
        {
            Character character = _characterRepo.GetCharacter(idPlayer, idCharacter);
            return Task.FromResult(character.RollAny(name));
        }

        [HttpGet("RollAttack")]
        public Task<int> RollAttack(int idPlayer, int idCharacter, int index)
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

        [HttpPost(Name = "CreateSpell")]
        public ActionResult Post(String description)
        {
            Spell test = new Spell { Description = description};
            _rpgContext.Spells.Add(test);
            _rpgContext.SaveChanges();

            return Ok(test);
        }

        [HttpPost("DeleteCharacter")]
        public IActionResult DeleteCharacter(int idPlayer, int idCharacter) {
            _characterRepo.DeleteCharacter(idPlayer, idCharacter);
            return StatusCode(200);
        }
    }
}
