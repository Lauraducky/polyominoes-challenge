namespace PolyominoesChallenge.Models;

public class PolyominoRow {
    private readonly string _stringRepresentation;

    public PolyominoRow(bool[] columns)
    {
        Columns = columns;
        _stringRepresentation = GetStringRepresentation();
    }

    public bool[] Columns { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as PolyominoRow);
    }

    public bool Equals(PolyominoRow? other){
        return other != null &&
            other._stringRepresentation.Equals(this._stringRepresentation);
    }

    public override int GetHashCode()
    {
        return _stringRepresentation.GetHashCode();
    }

    public override string ToString()
    {
        return _stringRepresentation;
    }

    private string GetStringRepresentation()
    {
        return string.Concat(Columns.Select(x => x ? "X" : " "));
    }
}