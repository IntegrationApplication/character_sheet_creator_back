using CharacterSheetCreatorBack.Classes;
using CharacterSheetCreatorBack.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CharacterSheetCreatorBack.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    public class CharacterController : ControllerBase
    {
        private readonly RpgContext _rpgContext;

        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ILogger<CharacterController> logger, RpgContext rpgContext)
        {
            _logger = logger;
            _rpgContext = rpgContext;
        }

        [HttpGet(Name = "GetCharacter")]
        public ActionResult Get(int ChannelID, int PlayerID)
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
            

            

            
        }


        [HttpPost(Name = "CreateCharacter")]
        public ActionResult Post(int ChannelID, int PlayerID)
        {
            Character newCharacter = new Character
            {
                IdPlayer = PlayerID,
                IdGame = ChannelID
            };

            _rpgContext.Characters.Add(newCharacter);
            _rpgContext.SaveChanges();

            var query = from b in _rpgContext.Characters
                        select b;


            return Ok(newCharacter);
        }
    }
}
   