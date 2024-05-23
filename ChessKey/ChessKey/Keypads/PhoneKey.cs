namespace ChessKeypad.App.Keypads
{
    public class PhoneKey : KeyBase
    {
        public override string[,] Layout => new string[,]
   {
        { "1","2","3" },
        { "4","5","6" },
        { "7","8","9" },
        { "*","0","#" }
   };
    }
}
