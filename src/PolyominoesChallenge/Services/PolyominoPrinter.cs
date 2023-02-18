using System.Text;
using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public class PolyominoPrinter : IPolyominoPrinter
{
    public void SavePolyominoesToFile(Polyomino[] polyominoes, string filePath)
    {
        var stringBuilder = new StringBuilder();
        foreach (var polyomino in polyominoes)
        {
            stringBuilder.AppendLine(PolyominoToText(polyomino));
        }

        File.WriteAllText(filePath, stringBuilder.ToString());
    }

    private string PolyominoToText(Polyomino polyomino)
    {
        var rows = polyomino.Rows.Select(x => PolyominoRowToText(x));
        return $"[{string.Join(", ", rows)}]";
    }

    private string PolyominoRowToText(PolyominoRow polyominoRow)
    {
        return $"[{string.Join(", ", polyominoRow.Columns)}]";
    }
}