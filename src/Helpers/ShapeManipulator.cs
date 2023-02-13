using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Helpers;

public class ShapeManipulator
{
    public Polyomino RotateShapeClockwise(Polyomino input)
    {
        var output = new Polyomino(new PolyominoRow[input.Rows[0].Columns.Length]);
        for (var i = 0; i < output.Rows.Length; i++)
        {
            var row = output.Rows[i];
            row.Columns = new bool[input.Rows.Length];
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
            var row = output.Rows[i];
            row.Columns = new bool[input.Rows[i].Columns.Length];
            for (var j = 0; j < row.Columns.Length; j++)
            {
                var inputColumn = row.Columns.Length - 1 - j;
                row.Columns[j] = input.Rows[i].Columns[inputColumn];
            }
        }

        return output;
    }
}
