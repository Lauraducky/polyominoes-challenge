using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IPolyominoValidator
{
    bool IsValidPolyomino(Polyomino polyomino, int size);
    Polyomino[] RemoveInvalidPolyominoes(Polyomino[] input, int size);
}
