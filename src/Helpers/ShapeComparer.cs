using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Helpers;

public class ShapeComparer
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
}