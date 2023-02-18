using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeManipulator : IShapeManipulator
{
    private static readonly Dictionary<Polyomino, Polyomino[]> AllShapeRotations = new();
    private static readonly Dictionary<Polyomino, Polyomino[]> AllEquivalentShapes = new();

    public Polyomino[] GetAllEquivalentShapes(Polyomino input, bool allowFlippedShapes)
    {
        if (AllEquivalentShapes.ContainsKey(input))
        {
            return AllEquivalentShapes[input];
        }

        var rotations = Enumerable.Range(0, 4).Select(x => RotateShapeMultipleTimes(input, x))
            .ToList();

        if (!allowFlippedShapes){
            var flippedShape = FlipShapeHorizontally(input);
            rotations.AddRange(Enumerable.Range(0, 4).Select(x => RotateShapeMultipleTimes(flippedShape, x)));
        }
        
        var output = rotations.Distinct().ToArray();
        AllEquivalentShapes[input] = output;
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
