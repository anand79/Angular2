using System.Text.RegularExpressions;

namespace ChessKeypad.App.Validators
{
    public class NumberValidator : IValidate
    {
        private readonly Regex _pattern = new("^\\d+$");
        public bool IsValid(string value)
        {
            return _pattern.IsMatch(value);
        }
    }
}
