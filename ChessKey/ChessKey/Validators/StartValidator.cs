namespace ChessKeypad.App.Validators
{
    public class StartValidator : IValidate
    {
        public bool IsValid(string value)
        {
            return !value.StartsWith("0") && !value.StartsWith("1");
        }
    }
}
