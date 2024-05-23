using ChessKeypad.App.Keypads;
using ChessKeypad.App.Validators;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Linq;

namespace ChessKeypad.App
{
    public class KeyNavigator
    {
        private readonly ILogger _log;
        private readonly KeyBase _keypad;
        private readonly List<IValidate> _validators;
        private int _maxLength;
        private int _totalCount;

        public KeyNavigator(KeyBase keypad, IEnumerable<IValidate> validators, ILogger<KeyNavigator> log)
        {
            _log = log;
            _keypad = keypad;
            _validators = validators.ToList();
        }

        public int Explore(ChessPiece chessPiece, int maxLength)
        {
            _log.LogInformation(@$"Start to explore {chessPiece.Name}");
            var layout = _keypad.Layout;

            _totalCount = 0;
            _maxLength = maxLength;

            if (layout != null
                && layout.GetLength(0) > 0
                && layout.GetLength(1) > 0)
            {
                // for each position, there is only limit numbers of positions it can reach in 1 step
                // calculate for effeciency
                Dictionary<(int, int), List<(int, int)>> dict = BuildNextPositionDictionary(chessPiece);

                int height = layout.GetLength(0);
                int width = layout.GetLength(1);

                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        _log.LogInformation("Start to navigate {i}, {j} \"{val}\"", i, j, layout[i, j]);
                        Navigate(i, j, string.Empty, dict);
                        _log.LogInformation("Finish navigating {i}, {j} \"{val}\"", i, j, layout[i, j]);

                    }
                }

                
            }
            _log.LogInformation("Finish explore {name}", chessPiece.Name);            
            return _totalCount;            
        }

        private Dictionary<(int, int), List<(int, int)>> BuildNextPositionDictionary(ChessPiece chessPiece)
        {
            Dictionary<(int, int), List<(int, int)>> dict = new();
            for (var i = 0; i < _keypad.Layout.GetLength(0); i++)
            {
                for (var j = 0; j < _keypad.Layout.GetLength(1); j++)
                {
                    dict[(i, j)] = _keypad.GetNextPositions(i, j, chessPiece);

                }
            }

            return dict;
        }

        private void Navigate(int i, int j, string empty, Dictionary<(int, int), List<(int, int)>> dict)
        {
            var output = empty + _keypad.Layout[i, j];

            var nextPositions = dict[(i, j)];

            if (!IsValid(output))
            {
                //invalid string
            }
            else if (output.Length == _maxLength)
            {
                _totalCount++;
            }
            else if (nextPositions == null || nextPositions.Count == 0)
            {
                // no moves left
            }
            else
            {
                foreach (var position in nextPositions)
                {
                    Navigate(position.Item1, position.Item2, output, dict);
                }
            }
        }

        private bool IsValid(string value)
        {
            return _validators == null || _validators.All(v => v.IsValid(value));
        }
    }
}
