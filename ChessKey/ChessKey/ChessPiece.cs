using ChessKeypad.App.Keypads;

namespace ChessKeypad.App
{
    public class ChessPiece
    {
        public string Name { get; private set; }

        private readonly List<(int, int)> _moves;

        private List<(int, int)> _movesList;

        private readonly bool _allowMultiple = false;
        public ChessPiece(string name, List<(int, int)> moves, bool allowMultiple)
        {
            Name = name;
            _moves = moves;
            _movesList = new List<(int, int)>();
            _allowMultiple = allowMultiple;

        }

        public List<(int, int)> GetValidMoves(KeyBase keypad)
        {
            if(keypad.Layout == null ||  keypad.Layout.Length == 0 || keypad.Layout.GetLength(1)==0)
            {
                throw new ArgumentNullException(nameof(keypad), "Invalid keypad layout");
            }
            if (_movesList.Count == 0)
            {
                // maxium number of steps a chess piece can make on a keypad
                var maxMove = _allowMultiple ? Math.Max(keypad.Layout.GetLength(0), keypad.Layout.GetLength(1)) - 1 : 1;
                _movesList = GetMoves(_moves, maxMove);
            }

            return _movesList;
        }

        private static List<(int, int)> GetMoves(List<(int, int)> steps, int size)
        {
            if (size <= 0)
            {
                throw new ArgumentNullException(nameof(size), "size has to be greater than 0");
            }

            if (steps == null || steps.Count < 1)
            {
                throw new ArgumentException("Invalid steps");
            }

            var multiplier = Enumerable.Range(1, size).ToList();

            return multiplier.SelectMany(m => steps.Select(s => (s.Item1 * m, s.Item2 * m))).ToList();

        }
    }
}
