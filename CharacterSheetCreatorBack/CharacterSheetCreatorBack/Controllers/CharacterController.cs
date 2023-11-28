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
        public async Task<List<Spell>> Get()
        {
            var query = from b in _rpgContext.Spells
                        select b;

            List<Spell> Spells = new List<Spell>();

            foreach (var item in query)
            {
                Spells.Add(item);
            }
            return Spells;
        }


        /*
        public async Task<Game> Get()
        {

            Game test = new Game();
            Character MJChar = new Character { Name = "Billy" };
            Player MJ = new Player { ID  = 1, IDDiscord = 18 , Characters = new List<Character>() };
            MJ.Characters.Add(MJChar);
            

            test.MJ = MJ;


            return test;
        }*/


        [HttpPost(Name = "CreateSpell")]
        public async Task<Spell> Post(String description)
        {
            Spell test = new Spell { Description = description};
            _rpgContext.Spells.Add(test);
            _rpgContext.SaveChanges();

            return test;
        }
    }
}
