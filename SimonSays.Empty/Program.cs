using SimonSays.Bll.Services;
using SimonSays.Bll.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISimonSaysService, SimonSaysService>();

var app = builder.Build();
app.MapGet("/", () => "-== Siamon says game ==-\n Endpoints:\n New game: /guess\n Guess the numbers: /guess/{number?}");

app.MapGet("/guess/{number?}", (int? number) =>
{
    var simonSaysService = app.Services.GetRequiredService<ISimonSaysService>();
    if (number.HasValue)
    {
        var (Correct, NextNumber) = simonSaysService.Guess(number.Value);
        if (Correct)
        {
            return NextNumber.HasValue
                   ? $"Great! The next number is {NextNumber}!"
                   : $"Number {simonSaysService.CorrectGuesses}: corret!";
        }
        else
        {
            var correctGuesses = simonSaysService.CorrectGuesses;
            var count = simonSaysService.Numbers.Count();
            var correctNumber = NextNumber.HasValue
                                ? NextNumber
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
