using CharacterSheetCreatorBack.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CharacterSheetCreatorBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<CharacterController> _logger;

        public CharacterController(ILogger<CharacterController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCharacter")]
        public async Task<Character> Get()
        {
            Character test = new Character();
            return test;
        }

        [HttpPost(Name = "CreateCharacter")]
        public async Task<Character> Post(String name, String playerID, String playerName, String background, String alignement, String race, String classAndLevel)
        {
            Character test = new Character(name, playerID, playerName,background,  alignement, race, classAndLevel);
            return test;
        }
    }
}
