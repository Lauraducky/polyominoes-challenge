namespace PolyominoesChallenge.Services;

public class IntegerArrayComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[]? x, int[]? y)
    {
        if (x == null && y == null)
        {
            return true;
        }

        if (x == null || y == null)
        {
            return false;
        }

        return x.SequenceEqual(y);
    }

    public int GetHashCode(int[] obj)
    {
        var hashCode = 17;
        foreach(var integer in obj)
        {
            hashCode ^= integer.GetHashCode();
        }

        return hashCode;
    }
}