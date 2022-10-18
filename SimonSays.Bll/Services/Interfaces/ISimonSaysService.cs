namespace SimonSays.Bll.Services.Interfaces
{
    public interface ISimonSaysService
    {
        IEnumerable<int> Numbers { get; }
        int CorrectGuesses { get; }
        (bool Correct, int? NextNumber) Guess(int number);
        int NewGame();
    }
}
