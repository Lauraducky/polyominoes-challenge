namespace PolyominoesChallenge.Models;

public class Polyomino {
    internal string stringRepresentation;
    public Polyomino(PolyominoRow[] rows)
    {
        Rows = rows;
        stringRepresentation = ToString();
    }

    public PolyominoRow[] Rows { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Polyomino);
    }

    public bool Equals(Polyomino? other){
        return other != null &&
            other.stringRepresentation.Equals(this.stringRepresentation);
    }

    public override int GetHashCode()
    {
        return stringRepresentation.GetHashCode();
    }

    public override string ToString()
    {
        return string.Join("\n", Rows.Select(x => x.ToString()));
    }
}