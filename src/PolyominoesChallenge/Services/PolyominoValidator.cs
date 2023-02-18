using System;
using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class PolyominoValidator : IPolyominoValidator
{
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
        var adjacentSquares = new Stack<Coordinate>();
        var floodedSquares = new List<Coordinate>();
        var height = polyomino.Rows.Length;
        var width = polyomino.Rows[0].Columns.Length;

        adjacentSquares.Push(new Coordinate {Y = 0, X = Array.IndexOf(polyomino.Rows[0].Columns, true)});
        do
        {
            var currentSquare = adjacentSquares.Pop();
            
            if (!polyomino.Rows[currentSquare.Y].Columns[currentSquare.X])
            {
                continue;
            }

            floodedSquares.Add(currentSquare);
            var neighbours = GetAdjacentSquares(polyomino, currentSquare, height, width)
                .Where(value => !floodedSquares.Contains(value) &&
                                !adjacentSquares.Contains(value));
            foreach (var neighbour in neighbours)
            {
                adjacentSquares.Push(neighbour);
            }
        }
        while(adjacentSquares.Count > 0);

        return floodedSquares.Count;
    }

    private List<Coordinate> GetAdjacentSquares(Polyomino polyomino, Coordinate current, int height, int width)
    {
        var response = new List<Coordinate>();
        if (current.Y > 0)
        {
            response.Add(new Coordinate{Y = current.Y - 1, X = current.X});
        }

        if (current.X > 0)
        {
            response.Add(new Coordinate{Y = current.Y, X = current.X - 1});
        }

        if (current.Y < height - 1)
        {
            response.Add(new Coordinate{Y = current.Y + 1, X = current.X});
        }

        if (current.X < width - 1)
        {
            response.Add(new Coordinate{Y = current.Y, X = current.X + 1});
        }

        return response;
    }
}