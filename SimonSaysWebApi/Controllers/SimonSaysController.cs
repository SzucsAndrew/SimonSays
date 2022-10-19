using Microsoft.AspNetCore.Mvc;
using SimonSays.Bll.Services.Interfaces;

namespace SimonSaysWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimonSaysController : ControllerBase
    {
        private ISimonSaysService _simonSaysService;

        public SimonSaysController(ISimonSaysService simonSaysService)
        {
            _simonSaysService = simonSaysService;
        }

        [HttpGet]
        public string Guess(int? number)
        {
            if (number.HasValue)
            {
                var (correct, nextNumber) = _simonSaysService.Guess(number.Value);
                if (correct)
                {
                    return nextNumber.HasValue
                           ? $"Great! The next number is {nextNumber}!"
                           : $"Number {_simonSaysService.CorrectGuesses}: correct!";
                }
                else
                {
                    var correctGuesses = _simonSaysService.CorrectGuesses;
                    var count = _simonSaysService.Numbers.Count();
                    var correctNumber = nextNumber.HasValue
                                        ? nextNumber
                                        : _simonSaysService.Numbers.ElementAt(correctGuesses);

                    return $"Oh no! The correct number was {correctNumber}. You guessed {correctGuesses} out of {count} correctly.";
                }
            }
            else
            {
                return $"New game! First number is {_simonSaysService.NewGame()}!";
            }
        }
    }
}
