using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IPolyominoPrinter
{
    void SavePolyominoesToFile(Polyomino[] polyominoes, string filePath);
}
