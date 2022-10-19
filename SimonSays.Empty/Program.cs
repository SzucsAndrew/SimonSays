using SimonSays.Bll.Services;
using SimonSays.Bll.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISimonSaysService, SimonSaysService>();

var app = builder.Build();
app.MapGet("/", () => "-== Simon says game ==-\n Endpoints:\n New game: /guess\n Guess the numbers: /guess/{number}");

app.MapGet("/guess/{number?}", (int? number) =>
{
    var simonSaysService = app.Services.GetRequiredService<ISimonSaysService>();
    if (number.HasValue)
    {
        var (correct, nextNumber) = simonSaysService.Guess(number.Value);
        if (correct)
        {
            return nextNumber.HasValue
                   ? $"Great! The next number is {nextNumber}!"
                   : $"Number {simonSaysService.CorrectGuesses}: correct!";
        }
        else
        {
            var correctGuesses = simonSaysService.CorrectGuesses;
            var count = simonSaysService.Numbers.Count();
            var correctNumber = nextNumber.HasValue
                                ? nextNumber
                                : simonSaysService.Numbers.ElementAt(correctGuesses);

            return $"Oh no! The correct number was {correctNumber}. You guessed {correctGuesses} out of {count} correctly.";
        }
    }
    else
    {
        return $"New game! First number is {simonSaysService.NewGame()}!";
    }
});

app.Run();
