using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class ShapeManipulator : IShapeManipulator
{
    public Polyomino GetStandardShapeRotation(Polyomino input)
    {
        var possibleShapes = Enumerable.Range(0, 4).Select(x => RotateShapeMultipleTimes(input, x))
            .Distinct().ToList();
        var output = possibleShapes[0];
        foreach (var shape in possibleShapes.Skip(0))
        {
            if (!output.Rows[0].Columns[0])
            {
                output = shape;
                continue;
            }

            if (!shape.Rows[0].Columns[0])
            {
                continue;
            }

            if (GetNumConsecutiveCells(shape.Rows[0]) > GetNumConsecutiveCells(output.Rows[0]))
            {
                output = shape;
            }
        }

        return output;
    }

    private int GetNumConsecutiveCells(PolyominoRow row)
    {
        var index = 0;
        while (index < row.Columns.Length && row.Columns[index])
        {
            index++;
        }

        return index;
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
        var output = new Polyomino(new PolyominoRow[input.Rows[0].Columns.Length]);
        for (var i = 0; i < output.Rows.Length; i++)
        {
            output.Rows[i] = new PolyominoRow(new bool[input.Rows.Length]);
            var row = output.Rows[i];
            for (var j = 0; j < row.Columns.Length; j++)
            {
                var inputRow = row.Columns.Length - 1 - j;
                row.Columns[j] = input.Rows[inputRow].Columns[i];
            }
        }

        return output;
    }

    public Polyomino FlipShapeHorizontally(Polyomino input)
    {
        var output = new Polyomino(new PolyominoRow[input.Rows.Length]);
        for (var i = 0; i < input.Rows.Length; i++)
        {
            output.Rows[i] = new PolyominoRow(new bool[input.Rows[i].Columns.Length]);
            var row = output.Rows[i];
            for (var j = 0; j < row.Columns.Length; j++)
            {
                var inputColumn = row.Columns.Length - 1 - j;
                row.Columns[j] = input.Rows[i].Columns[inputColumn];
            }
        }

        return output;
    }
}
