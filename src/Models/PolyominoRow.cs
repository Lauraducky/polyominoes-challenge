namespace PolyominoesChallenge.Models;

public class PolyominoRow {
    public PolyominoRow(bool[] columns)
    {
        Columns = columns;
    }

    public bool[] Columns { get; set; }
}