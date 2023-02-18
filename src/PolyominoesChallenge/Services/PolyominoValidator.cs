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
        var width = polyomino.Rows[0].Columns.Length;
        var height = polyomino.Rows.Length;

        var maxY = height - 1;
        var columns = new int[width];
        var diagonal1 = new int[width + height - 1];
        var diagonal2 = new int[width + height - 1];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var value = polyomino.Rows[y].Columns[x] ? 1 : 0;
                columns[x] += value;
                diagonal1[x + y] += value;
                diagonal2[maxY - y + x] = value;
                if (polyomino.Rows[y].Columns[x] && 
                    (y == 0 || !polyomino.Rows[y-1].Columns[x]) &&
                    (y == height - 1 || !polyomino.Rows[y+1].Columns[x]) &&
                    (x == 0 || !polyomino.Rows[y].Columns[x-1]) &&
                    (x == width - 1 || !polyomino.Rows[y].Columns[x+1]))
                {
                    return false;
                }
            }
        }

        if (columns.Any(x => x == 0))
        {
            return false;
        }

        if (diagonal1.Any(x => x == 0 && diagonal1.Take(x).Any(y => y > 0) && diagonal1.Skip(x + 1).Any(y => y > 0)))
        {
            return false;
        }

        if (diagonal2.Any(x => x == 0 && diagonal2.Take(x).Any(y => y > 0) && diagonal2.Skip(x + 1).Any(y => y > 0)))
        {
            return false;
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
            
            if (!polyomino.Rows[y].Columns[x])
            {
                continue;
            }

            floodedSquares.Add(currentSquare);
            var neighbours = GetAdjacentSquares(polyomino, y, x, height, width)
                .Where(value => !floodedSquares.Contains(value, _integerArrayComparer) &&
                                !adjacentSquares.Contains(value, _integerArrayComparer));
            adjacentSquares.AddRange(neighbours);
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