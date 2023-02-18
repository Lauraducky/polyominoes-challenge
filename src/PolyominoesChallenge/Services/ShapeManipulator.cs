using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeManipulator : IShapeManipulator
{
    private static readonly Dictionary<Polyomino, Polyomino[]> AllShapeRotations = new();

    public Polyomino[] GetAllShapeRotations(Polyomino input)
    {
        if (AllShapeRotations.ContainsKey(input))
        {
            return AllShapeRotations[input];
        }

        var output = Enumerable.Range(0, 4).Select(x => RotateShapeMultipleTimes(input, x))
            .Distinct().ToArray();
        foreach(var polyomino in output)
        {
            AllShapeRotations[polyomino] = output;
        }
        
        return output;
    }

    private Polyomino RotateShapeMultipleTimes(Polyomino input, int times)
    {
        var output = input;
        for (var i = 0; i < times; i++)
        {
            output = RotateShapeClockwise(output);
        }

        return output;
    }

    public Polyomino RotateShapeClockwise(Polyomino input)
    {
        var output = new bool[input.Rows[0].Columns.Length][];
        for (var i = 0; i < output.Length; i++)
        {
            output[i] = new bool[input.Rows.Length];
            var row = output[i];
            for (var j = 0; j < row.Length; j++)
            {
                var inputRow = row.Length - 1 - j;
                row[j] = input.Rows[inputRow].Columns[i];
            }
        }

        return new Polyomino(output.Select(x => new PolyominoRow(x)).ToArray());
    }

    public Polyomino FlipShapeHorizontally(Polyomino input)
    {
        return new Polyomino(input.Rows.Select(x => new PolyominoRow(x.Columns.Reverse().ToArray())).ToArray());
    }
}
