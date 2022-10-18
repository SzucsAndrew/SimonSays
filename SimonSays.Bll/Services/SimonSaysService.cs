using SimonSays.Bll.Services.Interfaces;

namespace SimonSays.Bll.Services
{
    public sealed class SimonSaysService : ISimonSaysService
    {
        private readonly Random _random = new();
        private readonly List<int> _numbers = new(10);
        public IEnumerable<int> Numbers => _numbers;
        public int CorrectGuesses { get; private set; } = 0;

        public (bool Correct, int? NextNumber) Guess(int number)
        {
            if (_numbers.Count == 0)
                return (false, 404); //Have fun

            if (!IsGoodGuess(number))
            {
                return (false, null);
            }

            if (AnyNumbersLeft())
            {
                CorrectGuesses++;
                return (true, null);
            }

            return (true, AddNewNumber());
        }

        public int NewGame()
        {
            _numbers.Clear();
            return AddNewNumber();
        }

        private int AddNewNumber()
        {
            //Lets genearte a new number and
            //start the guessing from the beginning

            CorrectGuesses = 0;
            var number = _random.Next(1, 99);
            _numbers.Add(number);

            return number;
        }

        private bool IsGoodGuess(int number)
        {
            return _numbers[CorrectGuesses] == number;
        }

        private bool AnyNumbersLeft()
        {
            return _numbers.Count - 1 != CorrectGuesses;
        }
    }
}
