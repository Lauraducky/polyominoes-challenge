using System;
using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class PolyominoValidator : IPolyominoValidator
{
    private readonly IEqualityComparer<int[]> _integerArrayComparer;

    public PolyominoValidator(IEqualityComparer<int[]> integerArrayComparer)
    {
        _integerArrayComparer = integerArrayComparer;
    }

    public Polyomino[] RemoveInvalidPolyominoes(Polyomino[] input, int size)
    {
        var output = new List<Polyomino>(input);
        for (var i = output.Count - 1; i > 0; i--)
        {
            var current = output[i];
            if (!IsValidPolyomino(input[i], size))
            {
                output.RemoveAt(i);
            }
        }

        return output.ToArray();
    }

    public bool IsValidPolyomino(Polyomino polyomino, int size)
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

        return FloodPolyomino(polyomino) == size;
    }

    private int FloodPolyomino(Polyomino polyomino)
    {
        var adjacentSquares = new List<int[]>();
        var floodedSquares = new List<int[]>();
        var height = polyomino.Rows.Length;
        var width = polyomino.Rows[0].Columns.Length;

        adjacentSquares.Add(new[]{0, Array.IndexOf(polyomino.Rows[0].Columns, true)});
        do
        {
            var currentSquare = adjacentSquares[0];
            adjacentSquares.RemoveAt(0);
            var y = currentSquare[0];
            var x = currentSquare[1];
            if (polyomino.Rows[y].Columns[x])
            {
                floodedSquares.Add(currentSquare);
                adjacentSquares.AddRange(GetAdjacentSquares(polyomino, y, x, height, width)
                    .Where(x => !floodedSquares.Contains(x, _integerArrayComparer)));
            }
        }
        while(adjacentSquares.Count > 0);

        return floodedSquares.Count;
    }

    private List<int[]> GetAdjacentSquares(Polyomino polyomino, int y, int x, int height, int width)
    {
        var response = new List<int[]>();
        if (y > 0)
        {
            response.Add(new[]{y - 1, x});
        }

        if (x > 0)
        {
            response.Add(new[]{y, x - 1});
        }

        if (y < height - 1)
        {
            response.Add(new[]{y + 1, x});
        }

        if (x < width - 1)
        {
            response.Add(new[]{y, x + 1});
        }

        return response;
    }
}