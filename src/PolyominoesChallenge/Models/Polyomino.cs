namespace PolyominoesChallenge.Models;

public class Polyomino {
    private readonly string _stringRepresentation;
    public Polyomino(PolyominoRow[] rows)
    {
        Rows = rows;
        _stringRepresentation = GetStringRepresentation();
    }

    public PolyominoRow[] Rows { get; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Polyomino);
    }

    private bool Equals(Polyomino? other){
        return other != null &&
            other._stringRepresentation.Equals(_stringRepresentation);
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
        return string.Join("\n", Rows.Select(x => x.ToString()));
    }
}