using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Tests;

public static class TestHelpers
{
    public static Polyomino GeneratePolyomino(string input, int inputWidth)
    {
        var rows = Enumerable.Range(0, input.Length / inputWidth)
            .Select(i => input.Substring(i * inputWidth, inputWidth))
            .Select(x => new PolyominoRow(Enumerable.Range(0, inputWidth).Select(y => x[y] == '1').ToArray()))
            .ToArray();
        return new Polyomino(rows);
    }
}