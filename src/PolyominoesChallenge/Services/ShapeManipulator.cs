using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeManipulator : IShapeManipulator
{
    private static Dictionary<Polyomino, Polyomino[]> _allShapeRotations = new Dictionary<Polyomino, Polyomino[]>();

    public Polyomino[] GetAllShapeRotations(Polyomino input)
    {
        if (_allShapeRotations.ContainsKey(input))
        {
            return _allShapeRotations[input];
        }

        var output = Enumerable.Range(0, 4).Select(x => RotateShapeMultipleTimes(input, x))
            .Distinct().ToArray();
        foreach(var polyomino in output)
        {
            _allShapeRotations[polyomino] = output;
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
        var output = new bool[input.Rows.Length][];
        for (var i = 0; i < input.Rows.Length; i++)
        {
            output[i] = new bool[input.Rows[i].Columns.Length];
            var row = output[i];
            for (var j = 0; j < row.Length; j++)
            {
                var inputColumn = row.Length - 1 - j;
                row[j] = input.Rows[i].Columns[inputColumn];
            }
        }

        return new Polyomino(output.Select(x => new PolyominoRow(x)).ToArray());
    }
}
