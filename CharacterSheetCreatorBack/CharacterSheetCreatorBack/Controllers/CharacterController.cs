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
            

            

            
        }


        [HttpPost(Name = "CreateSpell")]
        public ActionResult Post(String description)
        {
            Spell test = new Spell { Description = description};
            _rpgContext.Spells.Add(test);
            _rpgContext.SaveChanges();

            return Ok(test);
        }
    }
}
