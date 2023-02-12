namespace PolyominoesChallenge.Helpers;

public class Polyomino {
    public List<PolyominoRow> Rows { get; set; }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}