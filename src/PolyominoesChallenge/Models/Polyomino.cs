namespace PolyominoesChallenge.Models;

public class Polyomino {
    public Polyomino(PolyominoRow[] rows)
    {
        Rows = rows;
    }

    public PolyominoRow[] Rows { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Polyomino);
    }

    public bool Equals(Polyomino? other){
        return other != null &&
            other.Rows.Length == Rows.Length &&
            other.Rows.SequenceEqual(Rows);
    }

    public override int GetHashCode()
    {
        var hashCode = 17;
        foreach(var row in Rows)
        {
            hashCode ^= row.GetHashCode();
        }

        return hashCode;
    }

    public override string ToString()
    {
        return string.Join("\n", Rows.Select(x => x.ToString()));
    }
}