namespace PolyominoesChallenge.Models;

public class PolyominoRow {
    public PolyominoRow(bool[] columns)
    {
        Columns = columns;
    }

    public bool[] Columns { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PolyominoRow);
    }

    public bool Equals(PolyominoRow? other){
        return other != null &&
            other.Columns.Length == Columns.Length &&
            other.Columns.SequenceEqual(Columns);
    }

    public override int GetHashCode()
    {
        var hashCode = 17;
        foreach(var column in Columns)
        {
            hashCode ^= column.GetHashCode();
        }

        return hashCode;
    }
}