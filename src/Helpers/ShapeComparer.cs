using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Helpers;

public class ShapeComparer : IEqualityComparer<Polyomino>
{
    public bool AreShapesEqual(Polyomino a, Polyomino b)
    {
        if (a.Rows.Length != b.Rows.Length)
        {
            return false;
        }

        for (var i = 0; i < a.Rows.Length; i++)
        {
            if (!a.Rows[i].Columns.SequenceEqual(b.Rows[i].Columns))
            {
                return false;
            }
        }

        return true;
    }

    bool IEqualityComparer<Polyomino>.Equals(Polyomino? x, Polyomino? y)
    {
        if (x == null && y == null)
        {
            return true;
        }

        if (x == null || y == null)
        {
            return false;
        }

        return AreShapesEqual(x, y);
    }

    int IEqualityComparer<Polyomino>.GetHashCode(Polyomino obj)
    {
        var hashCode = 17;
        foreach (var row in obj.Rows)
        {
            foreach (var column in row.Columns)
            {
                hashCode = hashCode * 31 + column.GetHashCode();
            }
        }
        
        return hashCode;
    }
}