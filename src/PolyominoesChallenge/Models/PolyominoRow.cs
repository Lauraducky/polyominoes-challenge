namespace PolyominoesChallenge.Models;

public class PolyominoRow {
    internal string stringRepresentation;

    public PolyominoRow(bool[] columns)
    {
        Columns = columns;
        stringRepresentation = ToString();
    }

    public bool[] Columns { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PolyominoRow);
    }

    public bool Equals(PolyominoRow? other){
        return other != null &&
            other.stringRepresentation.Equals(this.stringRepresentation);
    }

    public override int GetHashCode()
    {
        return stringRepresentation.GetHashCode();
    }

    public override string ToString()
    {
        return string.Concat(Columns.Select(x => x ? "X" : " "));
    }
}