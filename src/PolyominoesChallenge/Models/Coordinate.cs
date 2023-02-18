namespace PolyominoesChallenge.Models;

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Coordinate);
    }

    private bool Equals(Coordinate? other)
    {
        return other != null &&
            other.X == X &&
            other.Y == Y;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}