namespace PolyominoesChallenge.Models;

public class Polyomino {
    public Polyomino(PolyominoRow[] rows)
    {
        Rows = rows;
    }

    public PolyominoRow[] Rows { get; set; }
}