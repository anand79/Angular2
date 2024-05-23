namespace ChessKeypad.App.Keypads
{
    public abstract class KeyBase
    {
        public abstract string[,] Layout { get; }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < Layout.GetLength(0) &&
                y >= 0 && y < Layout.GetLength(1);
        }

        public List<string> GetSequence(List<(int x, int y)> sequences)
        {
            if(sequences== null || sequences.Count == 0)
            {
                throw new ArgumentNullException(nameof(sequences), "Invalid sequence");
            }
            return sequences.Where(s => IsValidPosition(s.x, s.y))
                .Select(s => (string)Layout.GetValue(s.x, s.y)!)
                .OrderBy(x => x)
                .ToList();
        }

        public List<(int, int)> GetNextPositions(int x, int y, ChessPiece piece)
        {
            if(piece == null) 
            {
                throw new ArgumentNullException(nameof(piece));
            }
            if(!IsValidPosition(x, y))
            {
                throw new ArgumentException("Invalid starting position");
            }
            var validMoves = piece.GetValidMoves(this);

            return validMoves.Where(m => IsValidPosition(x + m.Item1, y + m.Item2)).Select(m => (x + m.Item1, y + m.Item2)).ToList();
        }
    }
}
