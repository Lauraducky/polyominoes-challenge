using PolyominoesChallenge.Models;

namespace PolyominoesChallenge.Services;

public interface IPolyominoValidator
{
    bool IsValidPolyomino(Polyomino polyomino);
    Polyomino[] RemoveInvalidPolyominoes(Polyomino[] input);
}
