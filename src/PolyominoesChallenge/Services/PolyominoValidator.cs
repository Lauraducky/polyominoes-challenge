using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class PolyominoValidator : IPolyominoValidator
{
    public Polyomino[] RemoveInvalidPolyominoes(Polyomino[] input)
    {
        var output = new List<Polyomino>(input);
        for (var i = output.Count - 1; i > 0; i--)
        {
            var current = output[i];
            if (!IsValidPolyomino(input[i]))
            {
                output.RemoveAt(i);
            }
        }

        return output.ToArray();
    }

    public bool IsValidPolyomino(Polyomino polyomino)
    {
        for(var j = 0; j < polyomino.Rows[0].Columns.Length; j++)
        {
            if (polyomino.Rows.All(x => !x.Columns[j]))
            {
                return false;
            }

            for (var i = 0; i< polyomino.Rows.Length; i++)
            {
                if (polyomino.Rows[i].Columns[j] && 
                    (i == 0 || !polyomino.Rows[i-1].Columns[j]) &&
                    (i == polyomino.Rows.Length - 1 || !polyomino.Rows[i+1].Columns[j]) &&
                    (j == 0 || !polyomino.Rows[i].Columns[j-1]) &&
                    (j == polyomino.Rows[i].Columns.Length - 1 || !polyomino.Rows[i].Columns[j+1]))
                {
                    return false;
                }
            }
        }

        return true;
    }
}