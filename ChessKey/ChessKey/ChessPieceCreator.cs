using System.Collections.ObjectModel;

namespace ChessKeypad.App
{
    public class ChessPieceCreator
    {
        private readonly List<ChessPiece> _lst = new()
            {
                new ChessPiece("Rook",
                            new List<(int, int)>()
                 {
                    (1,0),
                    (0,1),
                    (-1,0),
                    (0,-1),
                 },true
                            ),

                new ChessPiece("Knight",
                        new List<(int, int)>()
                {   (1,2),
                    (1,-2),
                    (-1,2),
                    (-1,-2),
                    (2,1),
                    (2,-1),
                    (-2,1),
                    (-2,-1),
                },false
                        ),

               new ChessPiece("Pawn",
                              new List<(int, int)>()
                  {
                     (1,0),
                  }, false
                              )

            , new ChessPiece("Queen",
                            new List<(int, int)>()
                {   (1,1),
                    (1,0),
                    (1,-1),
                    (-1,1),
                    (-1,0),
                    (-1,-1),
                    (0,1),
                    (0,-1),
                }, true
                            )

            ,new ChessPiece("Bishop",
                new List<(int, int)>()
                {
                    (1,1),
                    (1,-1),
                    (-1,1),
                    (-1,-1),
                }, true
                )

            , new ChessPiece("King",
                            new List<(int, int)>()
                {
                    (1,1),
                    (1,0),
                    (1,-1),
                    (-1,1),
                    (-1,0),
                    (-1,-1),
                    (0,1),
                    (0,-1),
                }, false
                            )
            };
        public ReadOnlyCollection<ChessPiece> GetAvailableChessPieces()
        {

            return _lst.AsReadOnly();
        }

    }
}
